﻿// <auto-generated />
using System;
using FizzBuzzGame.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FizzBuzzGame.Server.Migrations
{
    [DbContext(typeof(FizzBuzzGameDbContext))]
    [Migration("20241222074843_uniqueGameName")]
    partial class uniqueGameName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FizzBuzzGame.Server.Models.Attempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CorrectNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("IncorrectNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Attempts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CorrectNumber = 3,
                            CreatedAt = new DateTime(2024, 12, 22, 7, 48, 42, 104, DateTimeKind.Utc).AddTicks(4879),
                            Duration = 60,
                            GameId = 1,
                            IncorrectNumber = 1,
                            IsCompleted = true,
                            Score = 5,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CorrectNumber = 2,
                            CreatedAt = new DateTime(2024, 12, 22, 7, 48, 42, 104, DateTimeKind.Utc).AddTicks(4891),
                            Duration = 45,
                            GameId = 2,
                            IncorrectNumber = 2,
                            IsCompleted = true,
                            Score = 4,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxRange")
                        .HasColumnType("integer");

                    b.Property<int>("MinRange")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxRange = 100,
                            MinRange = -50,
                            Name = "FooBooLoo"
                        },
                        new
                        {
                            Id = 2,
                            MaxRange = 50,
                            MinRange = -30,
                            Name = "FizzBuzz"
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.GameRule", b =>
                {
                    b.Property<int>("Divisor")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Divisor", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GameRules");

                    b.HasData(
                        new
                        {
                            Divisor = 7,
                            GameId = 1,
                            Description = "Replace numbers divisible by 7 with 'Foo'",
                            Word = "Foo"
                        },
                        new
                        {
                            Divisor = 11,
                            GameId = 1,
                            Description = "Replace numbers divisible by 11 with 'Boo'",
                            Word = "Boo"
                        },
                        new
                        {
                            Divisor = 103,
                            GameId = 1,
                            Description = "Replace numbers divisible by 103 with 'Loo'",
                            Word = "Loo"
                        },
                        new
                        {
                            Divisor = 3,
                            GameId = 2,
                            Description = "Replace numbers divisible by 3 with 'Fizz'",
                            Word = "Fizz"
                        },
                        new
                        {
                            Divisor = 5,
                            GameId = 2,
                            Description = "Replace numbers divisible by 5 with 'Buzz'",
                            Word = "Buzz"
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.OwnerShip", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "GameId");

                    b.ToTable("OwnerShips");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            GameId = 1
                        },
                        new
                        {
                            UserId = 1,
                            GameId = 2
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "create games, play games",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "play games",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alice",
                            Password = "hashedpassword1",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bob",
                            Password = "hashedpassword2",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.Attempt", b =>
                {
                    b.HasOne("FizzBuzzGame.Server.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FizzBuzzGame.Server.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.GameRule", b =>
                {
                    b.HasOne("FizzBuzzGame.Server.Models.Game", "Game")
                        .WithMany("Rules")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.OwnerShip", b =>
                {
                    b.HasOne("FizzBuzzGame.Server.Models.User", null)
                        .WithMany("OwnerShips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.User", b =>
                {
                    b.HasOne("FizzBuzzGame.Server.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.Game", b =>
                {
                    b.Navigation("Rules");
                });

            modelBuilder.Entity("FizzBuzzGame.Server.Models.User", b =>
                {
                    b.Navigation("OwnerShips");
                });
#pragma warning restore 612, 618
        }
    }
}
