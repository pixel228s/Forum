using AutoMapper;
using Forum.Application.Exceptions;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Commands.Registration
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager
                .CreateAsync(user, request.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                string message = string.Join("; ", result.Errors.Select(e => e.Description));
                Console.WriteLine(message);
                throw new RegistrationException(message : message);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return result;
        }
    }
}
