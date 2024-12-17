using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace FizzBuzzGame.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name {  get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        //foreign keys
        [Required]
        public int RoleId { get; set; }

        //navigation properties
        public Role Role {  get; set; }
        public List<OwnerShip> OwnerShips { get; set; }
    }
}
