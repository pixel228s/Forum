using AutoMapper;
using Forum.Application.Common.Dtos.Users.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper; 

        public UpdateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            if (request.Email != null)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    throw new ConflictException("User with this Email/Username already exists.");
                }
                user.Email = request.Email;
            }

            if (request.UserName != null)
            {
                var existingUser = await _userManager.FindByNameAsync(request.UserName);
                if (existingUser != null)
                {
                    throw new ConflictException("User with this Email/Username already exists.");
                }
                user.UserName = request.UserName;
            }

            await _userManager.UpdateAsync(user).ConfigureAwait(false);

            return _mapper.Map<UserResponse>(user);
        }
    }
}
