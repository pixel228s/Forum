using MediatR;

namespace Forum.Application.Features.UserFeatures.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
