using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Interfaces.IRepositories
{
    public interface IGameRepository
    {
        public Task<Game> GetGameByIdAsync(int gameId);
        public Task<IEnumerable<Game>> GetAllGamesAsync();
        public Task<Game> AddGameAsync(Game game);
        public Task<Game> UpdateGameAsync(Game game);
        public Task<bool> DeleteGameAsync(int gameId);
    }
}
