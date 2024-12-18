using FizzBuzzGame.Server.Data;
using FizzBuzzGame.Server.Interfaces.IRepositories;
using FizzBuzzGame.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Repositories
{
    public class AttemptRepository : IAttemptRepository
    {
        private readonly FizzBuzzGameDbContext _context;
        private readonly ILogger<AttemptRepository> _logger;

        public AttemptRepository(FizzBuzzGameDbContext context, ILogger<AttemptRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Attempt> GetAttemptByIdSAsync(int attemptId)
        {
            try
            {
                return await _context.Attempts.FirstOrDefaultAsync(a => a.Id == attemptId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Error in GetAttemptByIdSAsync: {msg}", ex.Message);
                throw;
            }
        }
        public async Task<Attempt> AddAttemptAsync(Attempt attempt)
        {
            try
            {
                if (attempt == null)
                {
                    throw new ArgumentNullException(nameof(attempt), "Try to add new attempt from empty object");
                }

                await _context.Attempts.AddAsync(attempt);
                await _context.SaveChangesAsync();

                return attempt;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Repository: Error in AddAttemptAsync: {msg}", ex.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Repository: Invalid input in AddAttemptAsync: {msg}", ex.Message);
                throw;
            }
        }
        public async Task<Attempt> UpdateAttemptAsync(Attempt attempt)
        {
            try
            {
                var existingAttempt = await _context.Attempts.FirstOrDefaultAsync(a => a.Id == attempt.Id);
                if (existingAttempt == null)
                {
                    return null;
                }

                existingAttempt.CorrectNumber = attempt.CorrectNumber;
                existingAttempt.IncorrectNumber = attempt.IncorrectNumber;
                existingAttempt.Score = attempt.Score;
                existingAttempt.IsCompleted = attempt.IsCompleted;

                await _context.SaveChangesAsync();
                return existingAttempt;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Repository: Error in UpdateAttemptAsync: {msg}", ex.Message);
                throw;
            }
        }
    }
}
