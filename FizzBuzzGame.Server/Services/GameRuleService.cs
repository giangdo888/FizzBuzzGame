using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Models;
using FizzBuzzGame.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FizzBuzzGame.Server.Services
{
    public class GameRuleService : IGameRuleService
    {
        private readonly IGameRuleRepository _gameRuleRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameRuleService> _logger;

        public GameRuleService(IGameRuleRepository gameRuleRepository, IGameRepository gameRepository, IMapper mapper, ILogger<GameRuleService> logger)
        {
            _gameRuleRepository = gameRuleRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StandAloneGameRuleDTO> CreateGameRuleAsync(StandAloneGameRuleDTO standAloneGameRuleDTO)
        {
            //validate input first
            if (standAloneGameRuleDTO == null)
            {
                _logger.LogWarning("Service: Missing input when creating new rule");
                throw new ArgumentNullException(nameof(standAloneGameRuleDTO));
            }
            if (!standAloneGameRuleDTO.GameId.HasValue)
            {
                _logger.LogWarning("Service: Missing GameId when creating new rule");
                throw new ArgumentException("GameId is required");
            }
            if (!standAloneGameRuleDTO.Divisor.HasValue)
            {
                _logger.LogWarning("Service: Missing Divisor when creating new rule");
                throw new ArgumentException("Divisor is required");
            }
            if (string.IsNullOrEmpty(standAloneGameRuleDTO.Word))
            {
                _logger.LogWarning("Service: Missing Word when creating new rule");
                throw new ArgumentException("Word is required");
            }

            //check the divisor range
            var game = await _gameRepository.GetGameByIdAsync(standAloneGameRuleDTO.GameId.Value);
            if (standAloneGameRuleDTO.Divisor < game.MinRange || standAloneGameRuleDTO.Divisor > game.MaxRange)
            {
                _logger.LogWarning("Service: Divisors is outside the range of MinRange - MaxRange");
                throw new ArgumentException("Invalid Divisor");
            }

            try
            {
                _logger.LogInformation("Serrvice: Creating new rule for game Id " + standAloneGameRuleDTO.GameId);
                var gameRule = _mapper.Map<GameRule>(standAloneGameRuleDTO);
                var result = await _gameRuleRepository.AddGameRuleAsync(gameRule);

                _logger.LogInformation("Serrvice: Successfully creating new rule for game Id " + standAloneGameRuleDTO.GameId);
                return _mapper.Map<StandAloneGameRuleDTO>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in CreateGameRuleAsync: {msg}", ex.Message);
                throw;
            }

        }
        public async Task<StandAloneGameRuleDTO> UpdateGameRuleAsync(StandAloneGameRuleDTO standAloneGameRuleDTO)
        {
            //validate input first
            if (standAloneGameRuleDTO == null)
            {
                _logger.LogWarning("Service: Missing input when updating new rule");
                throw new ArgumentNullException(nameof(standAloneGameRuleDTO));
            }
            if (!standAloneGameRuleDTO.GameId.HasValue)
            {
                _logger.LogWarning("Service: Missing GameId when updating new rule");
                throw new ArgumentException("GameId is required");
            }
            if (!standAloneGameRuleDTO.Divisor.HasValue)
            {
                _logger.LogWarning("Service: Missing Divisor when updating new rule");
                throw new ArgumentException("Divisor is required");
            }
            if (string.IsNullOrEmpty(standAloneGameRuleDTO.Word))
            {
                _logger.LogWarning("Service: Missing Word when updating new rule");
                throw new ArgumentException("Word is required");
            }

            try
            {
                _logger.LogInformation("Serrvice: Updating rule with game Id {id}", standAloneGameRuleDTO.GameId);
                var gameRule = _mapper.Map<GameRule>(standAloneGameRuleDTO);
                var result = await _gameRuleRepository.UpdateGameRuleAsync(gameRule);

                _logger.LogInformation("Serrvice: Successfully updating rule with game Id {id}", standAloneGameRuleDTO.GameId);
                return _mapper.Map<StandAloneGameRuleDTO>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in UpdateGameRuleAsync: {msg}", ex.Message);
                throw;
            }
        }
        public async Task<bool> DeleteGameRuleAsync(int gameId, int divisor)
        {
            try
            {
                _logger.LogInformation("Service: Attempt to delete rule with game Id {id}", gameId);
                return await _gameRuleRepository.DeleteGameRuleAsync(gameId, divisor);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in DeleteGameRuleAsync: {msg}", ex.Message);
                throw;
            }
        }
    }
}
