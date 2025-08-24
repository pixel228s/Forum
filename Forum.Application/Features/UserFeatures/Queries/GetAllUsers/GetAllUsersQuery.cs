using Forum.Application.Common.Dtos.Users.Responses;
using Forum.Domain.Parameters;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
        public RequestParameters parameters { get; set; }
    }
}
