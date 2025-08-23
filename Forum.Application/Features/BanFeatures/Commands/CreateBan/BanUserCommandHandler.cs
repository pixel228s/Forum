using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Forum.Application.Features.AdminFeatures.Commands.BanUser
{
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand, BanInfoResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IBanRepository _banRepository;
        private readonly IDistributedCache _distributedCache;

        public BanUserCommandHandler(UserManager<User> userManager,
            IMapper mapper,
            IBanRepository banRepository,
            IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _banRepository = banRepository;
            _distributedCache = distributedCache;
        }

        public async Task<BanInfoResponse> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString()).ConfigureAwait(false);

            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            string key = $"is-banned:{user.Id}";
            var isBanned = await _distributedCache.GetStringAsync(key)
                        .ConfigureAwait(false);
            if (isBanned != null)
            {
                throw new ConflictException("This user is already banned");
            }

            var ban = _mapper.Map<Ban>(request);

            await _banRepository.AddAsync(ban, cancellationToken).ConfigureAwait(false);

            var ttl = request.BanEndDate - DateTime.UtcNow;
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            };
            await _distributedCache.SetStringAsync(key, "true", options, cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
