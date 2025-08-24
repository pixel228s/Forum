using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Domain.Parameters;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Queries.GetAllBans
{
    public record GetAllBansQuery(RequestParameters parameters) : IRequest<IEnumerable<BanInfoResponse>>;
}
