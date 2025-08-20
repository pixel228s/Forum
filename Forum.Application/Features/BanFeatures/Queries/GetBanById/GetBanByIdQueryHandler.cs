using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Queries.GetBanById
{
    public class GetBanByIdQueryHandler : IRequestHandler<GetBanByIdQuery, BanInfoResponse>
    {
        private readonly IBanRepository _banRepository;
        private readonly IMapper _mapper;
        public GetBanByIdQueryHandler(IBanRepository banRepository, IMapper mapper)
        {
            _banRepository = banRepository;
            _mapper = mapper;
        }

        public async Task<BanInfoResponse> Handle(GetBanByIdQuery request, CancellationToken cancellationToken)
        {
            var ban = await _banRepository.GetBanById(request.Id, cancellationToken)
                .ConfigureAwait(false);
            if (ban == null)
            {
                throw new ObjectNotFoundException("Ban with this id doesn't exist");
            }

            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
