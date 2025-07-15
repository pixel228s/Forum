using Forum.Application.Abstractions.Messaging;
using Forum.Application.Features.UserFeatures.Dtos;

namespace Forum.Application.Features.UserFeatures.Queries
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUser>
    {
        public GetUserByIdQueryHandler()
        {
        }

        public Task<GetUser> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetUser
            {
                Id = query.Id,
                UserName = "SAlome"
            });
        }
    }
}
