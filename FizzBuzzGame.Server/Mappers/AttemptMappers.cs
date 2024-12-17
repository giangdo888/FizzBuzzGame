using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Mappers
{
    public class AttemptMappers : Profile
    {
        public AttemptMappers()
        {
            CreateMap<AttemptDTO, Attempt>();
            CreateMap<Attempt, AttemptDTO>();

            CreateMap<ResultDTO, Attempt>();
            CreateMap<Attempt, ResultDTO>();
        }
    }
}
