using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Interfaces.IRepositories
{
    public interface IGameRuleRepository
    {
        public Task<GameRule> AddGameRuleAsync(GameRule gameRule);
        public Task<GameRule> UpdateGameRuleAsync(GameRule gameRule);
        public Task<bool> DeleteGameRuleAsync(int gameId, int divisor);
    }
}
