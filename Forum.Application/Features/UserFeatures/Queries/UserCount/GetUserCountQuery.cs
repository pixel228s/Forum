using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.UserCount
{
    public record GetUserCountQuery() : IRequest<int>;
}
