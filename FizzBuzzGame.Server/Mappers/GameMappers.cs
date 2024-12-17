using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Mappers
{
    public class GameMappers : Profile
    {
        public GameMappers()
        {
            CreateMap<GameDTO, Game>();
            CreateMap<Game, GameDTO> ();

            CreateMap<DisplayGameDTO, Game>();
            CreateMap<Game, DisplayGameDTO>();
        }
    }
}
