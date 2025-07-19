using Forum.Application.Features.UserFeatures.Queries.Models;
using Forum.Application.messaging.Queries;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public record GetUserByIdQuery(int UserID) : IQuery<UserResponse>;
}
