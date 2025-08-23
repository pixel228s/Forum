using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Requests;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Domain.Entities.Posts;
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

            CreateMap<PostWithCommentCount, PostResponse>()
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.commentCount))
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.authorUsername))
                .ForMember(dest => dest.UserProfilePicUrl, opt => opt.MapFrom(src => src.authorPfp))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.post.UserId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.post.Content))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.post.Title))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.post.ImageUrl))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.post.Status))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.post.State))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.post.Id))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.post.CreatedAt));
        }
      
    }
}
