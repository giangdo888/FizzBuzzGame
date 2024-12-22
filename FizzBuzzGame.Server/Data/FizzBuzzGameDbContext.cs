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
            modelBuilder.Entity<Game>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<GameRule>().HasKey(
                r => new { r.Divisor, r.GameId }
            );

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
                new Game { Id = 1, Name = "FooBooLoo", MinRange = -50, MaxRange = 100 },
                new Game { Id = 2, Name = "FizzBuzz", MinRange = -30, MaxRange = 50 }
            );

            // Seed data for GameRules (for each game)
            modelBuilder.Entity<GameRule>().HasData(
                new GameRule {GameId = 1, Divisor = 7, Word = "Foo", Description = "Replace numbers divisible by 7 with 'Foo'" },
                new GameRule {GameId = 1, Divisor = 11, Word = "Boo", Description = "Replace numbers divisible by 11 with 'Boo'" },
                new GameRule {GameId = 1, Divisor = 103, Word = "Loo", Description = "Replace numbers divisible by 103 with 'Loo'" },

                new GameRule {GameId = 2, Divisor = 3, Word = "Fizz", Description = "Replace numbers divisible by 3 with 'Fizz'" },
                new GameRule {GameId = 2, Divisor = 5, Word = "Buzz", Description = "Replace numbers divisible by 5 with 'Buzz'" }
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
                new Attempt { Id = 1, UserId = 1, GameId = 1, CorrectNumber = 3, IncorrectNumber = 1, Duration = 60, Score = 5, IsCompleted = true },
                new Attempt { Id = 2, UserId = 2, GameId = 2, CorrectNumber = 2, IncorrectNumber = 2, Duration = 45, Score = 4, IsCompleted = true }
            );
        }
    }
}
