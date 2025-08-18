using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Features.AdminFeatures.Commands.BanUser;
using Forum.Domain.Models;

namespace Forum.Application.Common.Mappers
{
    public class BanMapper : Profile
    {
        public BanMapper() 
        {
            CreateMap<BanUserCommand, Ban>();

            CreateMap<Ban, BanInfoResponse>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
