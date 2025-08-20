using Forum.Application.Common.Dtos.BanInfo.Responses;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Commands.BanUser
{
    public class BanUserCommand : IRequest<BanInfoResponse>
    {
        public int UserId { get; set; }
        public string BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
    }
}
