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
        private readonly ITransactionFactory _transactionFactory;
        private readonly IDistributedCache _distributedCache;

        public BanUserCommandHandler(UserManager<User> userManager, 
            IMapper mapper,
            IBanRepository banRepository,
            ITransactionFactory transactionFactory,
            IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _banRepository = banRepository;
            _transactionFactory = transactionFactory;
            _distributedCache = distributedCache;
        }

        public async Task<BanInfoResponse> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString()).ConfigureAwait(false);

            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            if (user.IsBanned)
            {
                throw new AppException("User is already banned");
            }

            user.IsBanned = true;
            var ban = _mapper.Map<Ban>(request); 
         
            using var transaction = await _transactionFactory.OpenTransactionAsync(cancellationToken)
                .ConfigureAwait(false);

            try
            {
                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    string message = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new AppException(message);
                }

                await _banRepository.AddAsync(ban, cancellationToken).ConfigureAwait(false);

                string key = $"is-banned:{user.Id}";
                await _distributedCache.SetStringAsync(key, "true", cancellationToken)
                    .ConfigureAwait(false);

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch(Exception) 
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            } 

            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
