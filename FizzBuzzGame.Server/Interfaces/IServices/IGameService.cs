using FizzBuzzGame.Server.DTOs;

namespace FizzBuzzGame.Server.Interfaces.IServices
{
    public interface IGameService
    {
        public Task<GameDTO> GetGameByIdAsync(int gameId);
        public Task<IEnumerable<GameDTO>> GetAllGamesAsync();
        public Task<GameDTO> CreateGameAsync(GameDTO gameDTO);
        public Task<GameDTO> UpdateGameAsync(GameDTO gameDTO);
        public Task<bool> DeleteGameAsync(int gameId);
    }
}
