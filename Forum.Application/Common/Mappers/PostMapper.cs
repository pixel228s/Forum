using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Requests;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Domain.Models.Posts;

namespace Forum.Application.Common.Mappers
{
    public class PostMapper : Profile
    {

        public PostMapper() 
        {
            CreateMap<CreatePostCommand, Post>();
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserProfilePicUrl, opt => opt.MapFrom(src => src.User.picUrl))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedAt));
            CreateMap<UpdatePostRequest, UpdatePostCommand>();
            CreateMap<CreatePostRequest, CreatePostCommand>();
        }
      
    }
}
