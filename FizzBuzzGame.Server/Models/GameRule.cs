using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzGame.Server.Models
{
    public class GameRule
    {
        [Required]
        public int Divisor { get; set; }
        [Required]
        public string Word { get; set; }

        //foreign keys
        public int GameId { get; set; }

        //navigation properties
        public Game Game { get; set; }

    }
}
