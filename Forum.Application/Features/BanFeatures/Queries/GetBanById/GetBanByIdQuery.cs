using Forum.Application.Common.Dtos.BanInfo.Responses;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Queries.GetBanById
{
    public class GetBanByIdQuery : IRequest<BanInfoResponse>
    {
        public int Id { get; set; }
    }
}
