using AutoMapper;
using Forum.Application.Features.UserFeatures.Queries.Models;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetUserById(request.UserID, cancellationToken)
                .ConfigureAwait(false);

            if (user == null)
            {
                throw new ApplicationException();
            }

            return _mapper.Map<UserResponse>(user);
        }
    }
}
