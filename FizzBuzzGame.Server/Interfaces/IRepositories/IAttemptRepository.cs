using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Interfaces.IRepositories
{
    public interface IAttemptRepository
    {
        public Task<Attempt> GetAttemptByIdSAsync(int attemptId);
        public Task<Attempt> AddAttemptAsync(Attempt attempt);
        public Task<Attempt> UpdateAttemptAsync(Attempt attempt);

    }
}
