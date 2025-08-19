using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Models.Users;
using Forum.Infrastructure.Services.S3;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Features.UserFeatures.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IS3Service _s3Service;
        private readonly IConfiguration _config;

        public DeleteImageCommandHandler(UserManager<User> userManager,
            IS3Service s3Service,
            IConfiguration config)
        {
            _userManager = userManager;
            _s3Service = s3Service;
            _config = config;
        }

        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new ObjectNotFoundException("User not found!");
            }

            if (user.picUrl == null)
            {
                throw new AppException("User has no profile picture");
            }

            string bucket = _config["AWS:BucketName"]!;
            string folder = _config["AWS:Folder"]!;
            string name = user.picUrl!;

            user.picUrl = null;

            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            if(!result.Succeeded)
            {
                string message = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new AppException(message: message);
            }

            await _s3Service.DeleteFile($"{folder}/{name}", bucket, cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
