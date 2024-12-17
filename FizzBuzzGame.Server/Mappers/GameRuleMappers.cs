using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Mappers
{
    public class GameRuleMappers : Profile
    {
        public GameRuleMappers()
        {
            CreateMap<GameRuleDTO, GameRule>();
            CreateMap<GameRule, GameRuleDTO>();

            CreateMap<StandAloneGameRuleDTO, GameRule>();
            CreateMap<GameRule, StandAloneGameRuleDTO>();
        }
    }
}
