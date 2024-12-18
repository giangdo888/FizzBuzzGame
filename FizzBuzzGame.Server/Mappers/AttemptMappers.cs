using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Mappers
{
    public class AttemptMappers : Profile
    {
        public AttemptMappers()
        {
            CreateMap<InitialAttemptDTO, Attempt>();
            CreateMap<Attempt, InitialAttemptDTO>();

            CreateMap<AttemptResultDTO, Attempt>();
            CreateMap<Attempt, AttemptResultDTO>();
        }
    }
}
