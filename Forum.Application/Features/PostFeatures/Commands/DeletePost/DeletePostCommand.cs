using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<Unit>
    {
        public int PostId { get; set; }
    }
}
