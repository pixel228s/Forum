using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using Forum.Infrastructure.Services.S3;
using Forum.Infrastructure.Services.S3.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Features.UserFeatures.Commands.UploadProfilePicture
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Unit>
    {
        private readonly IS3Service _s3Service;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public UploadImageCommandHandler(IS3Service s3Service,
            IUserRepository userRepository,
            UserManager<User> userManager,
            IConfiguration config)
        {
            _s3Service = s3Service;
            _userRepository = userRepository;
            _userManager = userManager;
            _config = config;
        }

        public async Task<Unit> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new ObjectNotFoundException("User not found");
            }

            string bucket = _config["AWS:BucketName"]!;
            string folder = _config["AWS:Folder"]!;

            if (!string.IsNullOrEmpty(user.picUrl))
            {
                await _s3Service
                    .DeleteFile($"{folder}/{user.picUrl}", bucket, cancellationToken)
                    .ConfigureAwait(false);
            }

            var uploadRequest = FileUploadRequest
                   .CreateImage(folder, request.Image);
            string? urlToStore = await _s3Service.UploadFile(uploadRequest, bucket, cancellationToken)
                .ConfigureAwait(false);

            if (urlToStore == null)
            {
                throw new AppException("Failed to upload picture");
            }

            user.picUrl = urlToStore;

            var result = await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
