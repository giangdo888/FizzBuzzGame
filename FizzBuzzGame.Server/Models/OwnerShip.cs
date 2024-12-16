using System.ComponentModel.DataAnnotations;

namespace FizzBuzzGame.Server.Models
{
    public class OwnerShip
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
