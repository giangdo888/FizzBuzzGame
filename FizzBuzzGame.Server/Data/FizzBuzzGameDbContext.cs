using FizzBuzzGame.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Data
{
    public class FizzBuzzGameDbContext : DbContext
    {
        public FizzBuzzGameDbContext(DbContextOptions<FizzBuzzGameDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameRule> GameRules { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OwnerShip> OwnerShips { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for OwnerShips table
            modelBuilder.Entity<OwnerShip>().HasKey(
                o => new { o.UserId, o.GameId }
            );

            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "create games, play games" },
                new Role { Id = 2, Name = "User", Description = "play games" }
            );

            // Seed data for Games (Range start can be negative)
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "FooBooLoo", MinRange = -50, MaxRange = 100, CountDownTime = 15 },
                new Game { Id = 2, Name = "FizzBuzz", MinRange = -30, MaxRange = 50, CountDownTime = 10 }
            );

            // Seed data for GameRules (for each game)
            modelBuilder.Entity<GameRule>().HasData(
                new GameRule { Id = 1, GameId = 1, Divisor = 7, Description = "Replace numbers divisible by 7 with 'Foo'" },
                new GameRule { Id = 2, GameId = 1, Divisor = 11, Description = "Replace numbers divisible by 11 with 'Boo'" },
                new GameRule { Id = 3, GameId = 1, Divisor = 103, Description = "Replace numbers divisible by 103 with 'Loo'" },

                new GameRule { Id = 4, GameId = 2, Divisor = 3, Description = "Replace numbers divisible by 3 with 'Fizz'" },
                new GameRule { Id = 5, GameId = 2, Divisor = 5, Description = "Replace numbers divisible by 5 with 'Buzz'" }
            );

            // Seed data for Users (make sure password is hashed, just example here)
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice", Password = "hashedpassword1", RoleId = 1 },  // Admin
                new User { Id = 2, Name = "Bob", Password = "hashedpassword2", RoleId = 2 }     // User
            );


            // Seed data for OwnerShips (Only Admin can own games)
            modelBuilder.Entity<OwnerShip>().HasData(
                new OwnerShip { UserId = 1, GameId = 1 },  // Alice (Admin) owns FooBooLoo
                new OwnerShip { UserId = 1, GameId = 2 }   // Alice (Admin) owns FizzBuzz
            );

            // Seed data for Attempts (Example attempts for users)
            modelBuilder.Entity<Attempt>().HasData(
                new Attempt { Id = 1, UserId = 1, GameId = 1, CorrectNumber = 3, IncorrectNumber = 1, Duration = 60, Score = 5 },
                new Attempt { Id = 2, UserId = 2, GameId = 2, CorrectNumber = 2, IncorrectNumber = 2, Duration = 45, Score = 4 }
            );
        }
    }
}
