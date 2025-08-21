using MediatR;

namespace Forum.Application.Features.BanFeatures.Commands.UnbanUser
{
    public class DeleteBanCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public int BanId { get; set; }
    }
}
