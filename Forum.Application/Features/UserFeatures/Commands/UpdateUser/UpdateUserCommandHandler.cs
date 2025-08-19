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

            user.Email = request.Email ?? user.Email;
            user.UserName = request.UserName ?? user.UserName;
            user.picUrl = request.PfpUrl ?? user.picUrl;

            await _userManager.UpdateAsync(user).ConfigureAwait(false);

            return _mapper.Map<UserResponse>(user);
        }
    }
}
