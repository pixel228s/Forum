using Amazon.Runtime.Internal;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Queries.GetAllBans
{
    public class GetAllBansQuery : IRequest<IEnumerable<BanInfoResponse>>
    {
    }
}
