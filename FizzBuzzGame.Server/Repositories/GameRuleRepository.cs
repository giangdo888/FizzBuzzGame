using FizzBuzzGame.Server.Data;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Repositories
{
    public class GameRuleRepository : IGameRuleRepository
    {
        private readonly FizzBuzzGameDbContext _context;
        private readonly ILogger<GameRuleRepository> _logger;

        public GameRuleRepository(FizzBuzzGameDbContext context, ILogger<GameRuleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GameRule> AddGameRuleAsync(GameRule gameRule)
        {
            try
            {
                if (gameRule == null)
                {
                    throw new ArgumentNullException(nameof(gameRule), "Try to add new rule from empty object");
                }

                await _context.GameRules.AddAsync(gameRule);
                await _context.SaveChangesAsync();

                return gameRule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "Repository: Error in AddGameRuleAsync: {msg}", ex.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Repository: Invalid input in AddGameRuleAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<GameRule> UpdateGameRuleAsync(GameRule updatedGameRule)
        {
            try
            {
                var existingGameRule = await _context.GameRules
                    .FirstOrDefaultAsync(r => r.GameId == updatedGameRule.GameId && r.Divisor == updatedGameRule.Divisor);
                if (existingGameRule == null)
                {
                    return null;
                }

                existingGameRule.Divisor = updatedGameRule.Divisor;
                existingGameRule.Word = updatedGameRule.Word;
                existingGameRule.Description = updatedGameRule.Description;

                await _context.SaveChangesAsync();
                return existingGameRule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Repository: Error in UpdateGameRuleAsync: {msg}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteGameRuleAsync(int gameId, int divisor)
        {
            try
            {
                var existingGameRule = await _context.GameRules
                    .FirstOrDefaultAsync(r => r.GameId == gameId && r.Divisor == divisor);
                if (existingGameRule == null)
                {
                    return false;
                }

                _context.GameRules.Remove(existingGameRule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Repository: Error in DeleteGameRuleAsync: {msg}", ex.Message);
                throw;
            }
        }
    }
}
