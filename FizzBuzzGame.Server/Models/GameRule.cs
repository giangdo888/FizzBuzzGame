using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzGame.Server.Models
{
    public class GameRule
    {
        [Key]
        public int Id { get; set; }
        public int Divisor { get; set; }
        public string Description { get; set; }

        //foreign keys
        public int GameId { get; set; }

    }
}
