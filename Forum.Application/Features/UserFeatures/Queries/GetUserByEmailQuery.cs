using Forum.Application.Features.UserFeatures.Queries.Models;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public record GetUserByEmailQuery(string email) : IRequest<UserResponse>;
}
