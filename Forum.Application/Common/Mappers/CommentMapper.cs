using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Domain.Entities.Comments;

namespace Forum.Application.Common.Mappers
{
    public class CommentMapper : Profile
    {
        public CommentMapper() 
        {
            CreateMap<CommentWithUserInfo, CommentResponseDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment.Text))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.Comment.CreatedAt));

        }
    }
}
