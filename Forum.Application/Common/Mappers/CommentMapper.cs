using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Requests;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Features.CommentFeatures.Commands.CreateComment;
using Forum.Application.Features.CommentFeatures.Commands.UpdateComment;
using Forum.Domain.Entities.Comments;

namespace Forum.Application.Common.Mappers
{
    public class CommentMapper : Profile
    {
        public CommentMapper() 
        {
            CreateMap<CommentWithUserInfo, CommentResponseDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment.Text))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.Comment.CreatedAt))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Comment.Id))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Comment.PostId))
                .ForMember(dest => dest.UserProfilePicUrl, opt => opt.MapFrom(src => src.UserProfilePicUrl));

            CreateMap<CreateCommentCommand, Comment>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Content));

            CreateMap<CreateCommentRequest, CreateCommentCommand>();

            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<UpdateCommentRequest, UpdateCommentCommand>();
        }
    }
}
