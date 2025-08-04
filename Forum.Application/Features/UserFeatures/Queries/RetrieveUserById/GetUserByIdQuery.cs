using Forum.Application.Common.Dtos.Users.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.RetrieveUserById
{
    public record GetUserByIdQuery(int UserID) : IRequest<UserResponse>;
}
