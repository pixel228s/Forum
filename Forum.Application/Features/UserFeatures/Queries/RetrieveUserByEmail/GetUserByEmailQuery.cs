using Forum.Application.Common.Dtos.Users.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.RetrieveUserByEmail
{
    public record GetUserByEmailQuery(string email) : IRequest<UserResponse>;
}
