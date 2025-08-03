using Forum.Application.Common.Dtos.Users.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public record GetUserByEmailQuery(string email) : IRequest<UserResponse>;
}
