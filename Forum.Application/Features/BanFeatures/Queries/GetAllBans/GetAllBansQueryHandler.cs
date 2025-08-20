using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Queries.GetAllBans
{
    public class GetAllBansQueryHandler : IRequestHandler<GetAllBansQuery, IEnumerable<BanInfoResponse>>
    {
        private readonly IBanRepository _banRepository;
        private readonly IMapper _mapper;
        public GetAllBansQueryHandler(IBanRepository banRepository, 
            IMapper mapper)
        {
            _banRepository = banRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BanInfoResponse>> Handle(GetAllBansQuery request, CancellationToken cancellationToken)
        {
            var bans = await _banRepository
                .GetAllBans(cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<BanInfoResponse>>(bans);
        }
    }
}
