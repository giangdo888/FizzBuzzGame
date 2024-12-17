using System.ComponentModel.DataAnnotations;

namespace FizzBuzzGame.Server.Models
{
    public class Attempt
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int Duration { get; set; }
        public int CorrectNumber { get; set; }
        public int IncorrectNumber { get; set; }
        public int Score { get; set; }

        //foreign keys
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameId { get; set; }

        //navigation properties
        public User User { get; set; }
        public Game Game {  get; set; }
    }
}
