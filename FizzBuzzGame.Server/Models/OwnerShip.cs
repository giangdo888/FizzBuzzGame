using System.ComponentModel.DataAnnotations;

namespace FizzBuzzGame.Server.Models
{
    public class OwnerShip
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
