using Forum.Application.Features.UserFeatures.Queries.Models;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public record GetUserByIdQuery(int UserID) : IRequest<UserResponse>;
}
