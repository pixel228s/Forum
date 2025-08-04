using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.UserFeatures.Queries.GetUserComments
{
    public class GetUserCommentsQueryHandler : IRequestHandler<GetUserCommentsQuery, IEnumerable<CommentResponseDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetUserCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponseDto>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository
                .GetCommentsByUserId(request.Id, cancellationToken)
                .ConfigureAwait(false);

           return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
        }
    }
}
