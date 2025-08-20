using Forum.Application.Common.Dtos.BanInfo.Responses;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Commands.UpdateBan
{
    public class UpdateBanCommand : IRequest<BanInfoResponse>
    {
        public int Id { get; set; }
        public string? BanReason { get; set; }
        public DateTime? BannedUntil { get; set; }
    }
}
