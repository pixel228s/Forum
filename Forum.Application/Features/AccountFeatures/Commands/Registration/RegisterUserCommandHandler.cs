using AutoMapper;
using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Models.Users;
using Forum.Infrastructure.Services.S3;
using Forum.Infrastructure.Services.S3.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Features.AccountFeatures.Commands.Registration
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public RegisterUserCommandHandler(
            UserManager<User> userManager, 
            IMapper mapper,
            IS3Service s3Service,
            IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
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
                throw new RegistrationException(message : message);
            }

            await _userManager.AddToRoleAsync(user, "Member")
                .ConfigureAwait(false);

            return result;
        }
    }
}
