using MediatR;

namespace Forum.Application.Features.PostFeatures.Commands.ChangeState
{
    public class ChangeStateCommand : IRequest<Unit>
    {
        public int PostId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
