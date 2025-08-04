using AutoMapper;
using Forum.Application.Common.Dtos.Users.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.RetrieveUserByEmail
{
    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetUserByEmail(request.email, cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
            {
                throw new ObjectNotFoundException();
            }

            return _mapper.Map<UserResponse>(user);
        }
    }
}
