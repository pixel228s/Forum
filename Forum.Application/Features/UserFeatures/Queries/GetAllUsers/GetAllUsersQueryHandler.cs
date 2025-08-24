using AutoMapper;
using Forum.Application.Common.Dtos.Users.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository
                .GetAllUsers(request.parameters, cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }
    }
}
