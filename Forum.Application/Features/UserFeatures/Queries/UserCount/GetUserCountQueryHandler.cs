using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.UserCount
{
    public class GetUserCountQueryHandler : IRequestHandler<GetUserCountQuery, int>
    {
        private readonly IUserRepository _userRepository;

        public GetUserCountQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _userRepository.GetUserNumber(cancellationToken).ConfigureAwait(false);
            return count;
        }
    }
}
