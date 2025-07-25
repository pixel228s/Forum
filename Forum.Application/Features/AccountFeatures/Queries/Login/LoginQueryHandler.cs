using AutoMapper;
using Forum.Application.Exceptions;
using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
using Forum.Application.Features.UserFeatures.Queries.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public LoginQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            if (user != null)
            {
                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.password);
                if (isPasswordCorrect)
                {
                    var response = _mapper.Map<AuthResponse>(user);
                    response.Token = "debilo bavshvo";
                    return response;
                }
            }
            throw new AuthenticationException();
        }
    }
}
