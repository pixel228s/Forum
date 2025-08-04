using Forum.Application.Common.Dtos.Users.Responses;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PfpUrl { get; set; }
    }
}
