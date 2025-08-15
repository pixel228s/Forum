using MediatR;

namespace Forum.Application.Features.UserFeatures.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public required string UserId { get; set; }
        public required string RequesterId { get; set; }
    }
}
