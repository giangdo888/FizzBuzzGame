using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Migrations;
using FizzBuzzGame.Server.Models;
using FizzBuzzGame.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Services
{
    public class AttemptState
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CorrectCount { get; set; } = 0;
        public int IncorrectCount { get; set; } = 0;
        public int Score { get; set; } = 0;
        public List<int> Questions { get; set; }
        public DateTime LastQuestionTime { get; set; }
        public int TimeLimitEachQuestion { get; set; }
        public int Duration { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public Dictionary<int, string> Rules { get; set; }

    }
    public class AttemptService : IAttemptService
    {
        private readonly IAttemptRepository _attemptRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AttemptService> _logger;
        private readonly Dictionary<int, AttemptState> _attemptState;

        public AttemptService(IAttemptRepository attemptRepository, IGameRepository gameRepository, IMapper mapper, ILogger<AttemptService> logger, Dictionary<int, AttemptState> attemptState)
        {
            _attemptRepository = attemptRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _logger = logger;
            _attemptState = attemptState;
        }

        public async Task<InittialAttemptQuestionDTO> CreateAndStartAttemptAsync(InitialAttemptDTO initialAttemptDTO)
        {
            //validate input first
            if (initialAttemptDTO == null)
            {
                _logger.LogWarning("Service: Missing input when creating new attempt");
                throw new ArgumentNullException(nameof(initialAttemptDTO));
            }
            if (!initialAttemptDTO.Duration.HasValue || initialAttemptDTO.Duration.Value < 60)
            {
                _logger.LogWarning("Service: Missing Duration or Duration < 60 seconds when creating new attempt");
                throw new ArgumentException("A Duration > 60 is required");
            }
            if (!initialAttemptDTO.GameId.HasValue)
            {
                _logger.LogWarning("Service: Missing GameId when creating new attempt");
                throw new ArgumentException("GameId is required");
            }

            try
            {
                _logger.LogInformation("Serrvice: Creating new attempt");
                var initAttempt = _mapper.Map<Attempt>(initialAttemptDTO);
                var result = await _attemptRepository.AddAttemptAsync(initAttempt);

                _logger.LogInformation("Serrvice: Successfully creating new attempt");

                //collect all data for this attempt and save as an AttemptState
                var game = await _gameRepository.GetGameByIdAsync(result.GameId);
                Dictionary<int, string> rules = [];
                foreach (var rule in game.Rules)
                {
                    rules[rule.Divisor] = rule.Word;
                }

                var randomNumber = GenerateRandomNumber(game.MinRange, game.MaxRange, []) ?? throw new InvalidOperationException("No valid numbers available to generate.");
                List<int> questions = [randomNumber];
                var timeLimit = CalculateLimitTime(rules.Count);

                var attemptState = new AttemptState
                {
                    Id = result.Id,
                    Duration = result.Duration,
                    Questions = questions,
                    LastQuestionTime = DateTime.UtcNow,
                    TimeLimitEachQuestion = timeLimit,
                    MinRange = game.MinRange,
                    MaxRange = game.MaxRange,
                    Rules = rules
                };

                _attemptState[result.Id] = attemptState;

                return new InittialAttemptQuestionDTO
                {
                    Id = result.Id,
                    Question = randomNumber,
                    TimeLimitEachQuestion = timeLimit
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in CreateAndStartAttemptAsync: {msg}", ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Service: Error in CreateAndStartAttemptAsync: {msg}", ex.Message);
                throw;
            }
        }
        public AttemptResultAndNewQuestionDTO HandleAttemptAsnwer(AttemptAnswerDTO attemptAnswerDTO)
        {
            if (attemptAnswerDTO == null)
            {
                _logger.LogWarning("Service: Missing input when answering");
                throw new ArgumentNullException(nameof(attemptAnswerDTO));
            }

            var currentAttemptState = _attemptState[attemptAnswerDTO.Id];

            //check if timeout for this game
            if ((DateTime.UtcNow - currentAttemptState.CreatedAt).TotalMilliseconds > currentAttemptState.Duration * 1000)
            {
                return new AttemptResultAndNewQuestionDTO
                {
                    IsCorrect = false,
                    IsTimeOut = true,
                    Question = new AttemptQuestionDTO()
                };
            }

            var randomNumber = GenerateRandomNumber(currentAttemptState.MinRange, currentAttemptState.MaxRange, currentAttemptState.Questions)
                 ?? throw new InvalidOperationException("No valid numbers available to generate.");

            //check if timeout for this answer
            if ((DateTime.UtcNow - currentAttemptState.LastQuestionTime).TotalMilliseconds > currentAttemptState.TimeLimitEachQuestion*1000)
            {
                currentAttemptState.Questions.Add(randomNumber);
                currentAttemptState.LastQuestionTime = DateTime.UtcNow;
                currentAttemptState.IncorrectCount++;
                _attemptState[attemptAnswerDTO.Id] = currentAttemptState;

                return new AttemptResultAndNewQuestionDTO
                {
                    IsCorrect = false,
                    IsTimeOut = true,
                    Question = new AttemptQuestionDTO
                    {
                        Id = attemptAnswerDTO.Id,
                        Question = randomNumber
                    }
                };
            }

            var rules = currentAttemptState.Rules;
            if (rules == null)
            {
                _logger.LogError("Service: Game with no rule");
                throw new Exception("Error: a game must have at least 1 rule");
            }

            bool result = false;
            List<string> answer = [];
            var currentQuestion = currentAttemptState.Questions.Last();
            foreach (var rule in rules)
            {
                if (currentQuestion % rule.Key == 0)
                {
                    answer.Add(rule.Value);
                }
            }
            if (answer.Count == 0)
            {
                result = currentQuestion.ToString().Equals(attemptAnswerDTO.Answer);
            }
            else
            {
                //remove word by word from player's answer, return correct if the string is empty (all correct) after the loop
                var playerAnswer = attemptAnswerDTO.Answer;
                foreach (var word in answer)
                {
                    if (playerAnswer.Contains(word))
                    {
                        playerAnswer = playerAnswer.Replace(word, "");
                    }
                }
                
                if (string.IsNullOrEmpty(playerAnswer))
                {
                    result = true;
                }
            }

            if (result)
            {
                currentAttemptState.CorrectCount++;
            }
            else
            {
                currentAttemptState.IncorrectCount++;
            }

            currentAttemptState.Questions.Add(randomNumber);
            currentAttemptState.LastQuestionTime = DateTime.UtcNow;
            _attemptState[attemptAnswerDTO.Id] = currentAttemptState;

            return new AttemptResultAndNewQuestionDTO
            {
                IsCorrect = result,
                IsTimeOut = false,
                Question = new AttemptQuestionDTO
                {
                    Id = attemptAnswerDTO.Id,
                    Question = randomNumber
                }
            };
        }
        public async Task<AttemptResultDTO> FinalizeAttemptAsync(int attemptId)
        {
            if (_attemptState.TryGetValue(attemptId, out var attemptState))
            {
                try
                {
                    var existingAttempt = await _attemptRepository.GetAttemptByIdSAsync(attemptId);

                    //update score and complete status
                    existingAttempt.CorrectNumber = attemptState.CorrectCount;
                    existingAttempt.IncorrectNumber = attemptState.IncorrectCount;
                    existingAttempt.IsCompleted = true;

                    var result = await _attemptRepository.UpdateAttemptAsync(existingAttempt);
                    return _mapper.Map<AttemptResultDTO>(result);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Service: Error in FinalizeAttemptAsync: {msg}", ex.Message);
                    throw;
                }
            }

            throw new Exception("Service: already finalize game Id " + attemptId);
        }
        public async Task<AttemptResultDTO> GetAttemptResultAsync(int attemptId)
        {
            try
            {
                _logger.LogInformation("Service: Fetching attempt result for ID {id}", attemptId);
                var attempt = await _attemptRepository.GetAttemptByIdSAsync(attemptId);
                if (attempt == null)
                {
                    _logger.LogWarning("Service: Attempt with Id {id} not found", attemptId);
                    return null;
                }

                _logger.LogInformation("Service: Successfully retrieved attempt with ID {id}", attemptId);
                return _mapper.Map<AttemptResultDTO>(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while fetching attempt with ID {id}", attemptId);
                throw;
            }
        }

        public int? GenerateRandomNumber(int minRange, int maxRange, List<int> excludeList)
        {
            var validNumbers = Enumerable.Range(minRange, maxRange - minRange + 1).Except(excludeList).ToList();
            if (validNumbers.Count != 0)
            {
                var random = new Random();
                return validNumbers[random.Next(validNumbers.Count)];
            }

            return null;
        }

        public int CalculateLimitTime(int numberOfRules)
        {
            return 5 + (numberOfRules - 1) * 2;
        }
    }
}
