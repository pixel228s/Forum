using AutoMapper;
using Forum.Application.Features.UserFeatures.Queries.Models;
using Forum.Application.messaging.Queries;
using Forum.Persistence.Data;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly ForumDbContext _forumDbContext;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(ForumDbContext forumDbContext, IMapper mapper)
        {
            _forumDbContext = forumDbContext;
            _mapper = mapper;
        }

        public async Task<UserResponse> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _forumDbContext.Users
                .FindAsync(query.UserID)
                .ConfigureAwait(false); 

            return _mapper.Map<UserResponse>(user);
        }
    }
}
