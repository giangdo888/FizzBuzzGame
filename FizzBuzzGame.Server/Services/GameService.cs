using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, IMapper mapper, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GameDTO> GetGameByIdAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Service: Fetching game with ID {id}", gameId);
                var game = await _gameRepository.GetGameByIdAsync(gameId);
                if (game == null)
                {
                    _logger.LogWarning("Service: Game with Id {id} not found", gameId);
                    return null;
                }

                _logger.LogInformation("Service: Successfully retrieved game with ID {id}", gameId);
                return _mapper.Map<GameDTO>(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while fetching game with ID {id}", gameId);
                throw;
            }
        }

        public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
        {
            try
            {
                _logger.LogInformation("Service: Fetching game list");
                var games = await _gameRepository.GetAllGamesAsync();
                if (!games.Any())
                {
                    _logger.LogWarning("Service: No game found");
                    return [];
                }

                _logger.LogInformation("Service: Successfully retrieved game list");
                return _mapper.Map<IEnumerable<GameDTO>>(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while fetching game list");
                throw;
            }
        }

        public async Task<GameDTO> CreateGameAsync(GameDTO gameDTO)
        {
            //validate input first
            if (gameDTO == null)
            {
                _logger.LogWarning("Service: Missing input when creating new game");
                throw new ArgumentNullException(nameof(gameDTO));
            }
            if (string.IsNullOrEmpty(gameDTO.Name))
            {
                _logger.LogWarning("Service: Missing Name when creating new game");
                throw new ArgumentException("Name is required");
            }
            if (!gameDTO.MinRange.HasValue)
            {
                _logger.LogWarning("Service: Missing MinRange when creating new game");
                throw new ArgumentException("MinRange is required");
            }
            if (!gameDTO.MaxRange.HasValue)
            {
                _logger.LogWarning("Service: Missing MaxRange when creating new game");
                throw new ArgumentException("MaxRange is required");
            }
            if (!gameDTO.CountDownTime.HasValue)
            {
                _logger.LogWarning("Service: Missing CountDownTime when creating new game");
                throw new ArgumentException("CountDownTime is required");
            }
            if(gameDTO.Rules == null || gameDTO.Rules.Count == 0)
            {
                _logger.LogWarning("Service: Missing Rules when creating new game");
                throw new ArgumentException("The game must have at least 1 rule");
            }

            try
            {
                _logger.LogInformation("Serrvice: Creating new game with name {name}", gameDTO.Name);
                var game = _mapper.Map<Game>(gameDTO);
                var result = await _gameRepository.AddGameAsync(game);

                _logger.LogInformation("Serrvice: Successfully creating new game with name {name}", gameDTO.Name);
                return _mapper.Map<GameDTO>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in CreateGameAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<GameDTO> UpdateGameAsync(GameDTO gameDTO)
        {
            //validate input
            if (gameDTO == null)
            {
                _logger.LogWarning("Service: Missing input when trying to update game");
                throw new ArgumentNullException(nameof(gameDTO));
            }
            if (!gameDTO.Id.HasValue)
            {
                _logger.LogWarning("Service: Missing Id when trying to update game");
                throw new ArgumentException("Id is required");
            }
            if (string.IsNullOrEmpty(gameDTO.Name) && !gameDTO.MinRange.HasValue && !gameDTO.MaxRange.HasValue && !gameDTO.CountDownTime.HasValue)
            {
                _logger.LogWarning("Service: Missing data when trying to update game with Id {id}", gameDTO.Id);
                throw new ArgumentException("Nothing to update");
            }

            try
            {
                _logger.LogInformation("Serrvice: Updating game with id {id}", gameDTO.Id);
                var game = _mapper.Map<Game>(gameDTO);
                var result = await _gameRepository.UpdateGameAsync(game);

                _logger.LogInformation("Serrvice: Successfully updating game with Id {id}", gameDTO.Id);
                return _mapper.Map<GameDTO>(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in UpdateGameAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteGameAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Service: Attempt to delete game with Id {id}", gameId);
                return await _gameRepository.DeleteGameAsync(gameId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Service: Error in DeleteGameAsync: {msg}", ex.Message);
                throw;
            }
        }
    }
}
