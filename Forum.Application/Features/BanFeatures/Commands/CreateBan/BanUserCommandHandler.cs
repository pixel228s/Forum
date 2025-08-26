using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models;
using Forum.Domain.Models.Users;
using Forum.Infrastructure.Implementations;
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
        private readonly ITransactionFactory _transactionFactory;
        public BanUserCommandHandler(UserManager<User> userManager,
            IMapper mapper,
            IBanRepository banRepository,
            IDistributedCache distributedCache,
            ITransactionFactory transactionFactory)
        {
            _userManager = userManager;
            _mapper = mapper;
            _banRepository = banRepository;
            _distributedCache = distributedCache;
            _transactionFactory = transactionFactory;
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

            user.IsBanned = true;
            var ban = _mapper.Map<Ban>(request);

            using var transaction = await _transactionFactory.OpenTransactionAsync(cancellationToken)
             .ConfigureAwait(false);
            try
            {
                await _banRepository.AddAsync(ban, cancellationToken).ConfigureAwait(false);

                var ttl = request.BanEndDate - DateTime.UtcNow;
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = ttl
                };
                await _distributedCache.SetStringAsync(key, "true", options, cancellationToken)
                    .ConfigureAwait(false);

                await _userManager.UpdateAsync(user).ConfigureAwait(false);

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
