using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Responses;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Posts;
using Forum.Infrastructure.Services.S3;
using Forum.Infrastructure.Services.S3.Models.Requests;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Features.PostFeatures.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IS3Service _s3Service;
        private readonly IConfiguration _config;
        public CreatePostCommandHandler(
            IPostRepository postRepository, 
            IMapper mapper,
            IS3Service s3Service,
            IConfiguration config
            )
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _s3Service = s3Service;
            _config = config;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            post.UserId = request.userId;

            if (request.Image != null)
            {
                string folder = _config["AWS:Folder"]!;
                string bucket = _config["AWS:BucketName"]!;
                var uploadRequest = FileUploadRequest
                    .CreateImage(folder, request.Image);
                string? urlToStore = await _s3Service.UploadFile(uploadRequest, bucket, cancellationToken);

                if (urlToStore == null)
                {
                    throw new AppException("Failed to upload picture");
                }

                post.ImageUrl = urlToStore;
            }
            await _postRepository.AddAsync(post, cancellationToken);

            return _mapper.Map<PostResponse>(post);
        }
    }
}
