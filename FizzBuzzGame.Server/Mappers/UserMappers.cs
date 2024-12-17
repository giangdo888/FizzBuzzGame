using AutoMapper;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Models;

namespace FizzBuzzGame.Server.Mappers
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
