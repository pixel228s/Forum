using Forum.Application.Common.Dtos.Users.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public record GetUserByIdQuery(int UserID) : IRequest<UserResponse>;
}
