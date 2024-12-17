using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.DTOs
{
    //main type to receive and respond
    public class GameDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? MinRange { get; set; }
        public int? MaxRange { get; set; }
        public int? CountDownTime { get; set; }
        public ICollection<GameRuleDTO> Rules { get; set; }
    }

    //to display game
    public class DisplayGameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountDownTime { get; set; }
    }
}
