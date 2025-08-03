using AutoMapper;
using Forum.Application.Common.Dtos.Users.Responses;
using Forum.Application.Features.AccountFeatures.Commands.Registration;
using Forum.Domain.Models.Users;

namespace Forum.Application.Common.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<RegisterUserCommand, User>();

            //CreateMap<User, TokenDto>()
            //    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
