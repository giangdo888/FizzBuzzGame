﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzGame.Server.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int MinRange { get; set; }
        [Required]
        public int MaxRange { get; set; }
        public int CountDownTime { get; set; }

        //navigation properties
        public ICollection<GameRule> Rules { get; set; }
    }
}