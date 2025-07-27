using AutoMapper;
using Forum.Application.Common.SecurityService;
using Forum.Application.Exceptions;
using Forum.Application.Features.AccountFeatures.Queries.Login.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Features.AccountFeatures.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;

        public LoginQueryHandler(UserManager<User> userManager, IMapper mapper, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            if (user != null)
            {
                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.password);
                if (isPasswordCorrect)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var tokenDto = await _tokenProvider.CreateToken(user, roles, populateDate: true);
                    return tokenDto;
                }
            }
            throw new AuthenticationException();
        }
    }
}
