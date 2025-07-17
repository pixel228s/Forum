using Forum.Application.Abstractions.Messaging;
using Forum.Application.Features.UserFeatures.Queries.Models;
using Forum.Domain.Models.Users;
using Forum.Persistence.Data;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly ForumDbContext _forumDbContext;


        public GetUserByIdQueryHandler(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _forumDbContext.Users
                .FindAsync(query.Id)
                .ConfigureAwait(false);

            

        }
    }
}
