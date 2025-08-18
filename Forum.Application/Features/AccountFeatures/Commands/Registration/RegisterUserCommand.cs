using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.Registration
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public readonly string Folder = "ForumTest/";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? Image { get; set; }
    }

}
