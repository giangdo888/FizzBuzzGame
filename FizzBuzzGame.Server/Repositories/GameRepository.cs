using AutoMapper;
using FizzBuzzGame.Server.Data;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly FizzBuzzGameDbContext _context;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(FizzBuzzGameDbContext context, ILogger<GameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            try
            {
                return await _context.Games
                    .Include(g => g.Rules)
                    .FirstOrDefaultAsync(g => g.Id == gameId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Error in GetGameByIdAsync: {msg}",ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            try
            {
                return await _context.Games
                    .Include(g => g.Rules)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Error in GetAllGamesAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<Game> AddGameAsync(Game game)
        {
            try
            {
                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game), "Try to add new game from empty object");
                }

                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();

                return game;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex,"Repository: Error in AddGameAsync: {msg}", ex.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Repository: Invalid input in AddGameAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<Game> UpdateGameAsync(Game updatedGame)
        {
            try
            {
                var existingGame = await _context.Games
                    .Include(g => g.Rules)
                    .FirstOrDefaultAsync(g => g.Id == updatedGame.Id);
                if (existingGame == null)
                {
                    return null;
                }

                existingGame.Name = updatedGame.Name;
                existingGame.MinRange = updatedGame.MinRange;
                existingGame.MaxRange = updatedGame.MaxRange;
                existingGame.CountDownTime = updatedGame.CountDownTime;

                //remove all Rules that belong to this gameId
                existingGame.Rules.RemoveAll(r => r.GameId == existingGame.Id);

                //add new Rules from updatedGame
                foreach (var updatedRule in updatedGame.Rules)
                {
                    existingGame.Rules.Add(updatedRule);
                }
                await _context.SaveChangesAsync();
                return existingGame;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Repository: Error in UpdateGameAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteGameAsync(int gameId)
        {
            try
            {
                var existingGame = await _context.Games
                    .Include(g => g.Rules)
                    .FirstOrDefaultAsync(g => g.Id == gameId);
                if ( existingGame == null )
                {
                    return false;
                }

                _context.Games.Remove(existingGame);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Repository: Error in DeleteGameAsync: {msg}", ex.Message);
                throw;
            }
        }
    }
}
