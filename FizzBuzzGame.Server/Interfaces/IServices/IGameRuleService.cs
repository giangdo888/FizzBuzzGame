using FizzBuzzGame.Server.DTOs;

namespace FizzBuzzGame.Server.Interfaces.IServices
{
    public interface IGameRuleService
    {
        public Task<StandAloneGameRuleDTO> CreateGameRuleAsync(StandAloneGameRuleDTO standAloneGameRuleDTO);
        public Task<StandAloneGameRuleDTO> UpdateGameRuleAsync(StandAloneGameRuleDTO standAloneGameRuleDTO);
        public Task<bool> DeleteGameRuleAsync(int gameId, int divisor);
    }
}
