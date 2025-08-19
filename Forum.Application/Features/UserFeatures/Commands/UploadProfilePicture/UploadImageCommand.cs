using MediatR;
using Microsoft.AspNetCore.Http;

namespace Forum.Application.Features.UserFeatures.Commands.UploadProfilePicture
{
    public class UploadImageCommand : IRequest<Unit>
    {
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
    }
}
