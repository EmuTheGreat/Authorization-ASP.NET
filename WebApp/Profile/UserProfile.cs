using AutoMapper;
using Logic.Models;
using API.Models.DTO.Request;
using API.Models.DTO.Response;

namespace API.Models
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterRequest>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, LoginRequest>();
            CreateMap<LoginRequest, User>();
            CreateMap<UserResponse, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
