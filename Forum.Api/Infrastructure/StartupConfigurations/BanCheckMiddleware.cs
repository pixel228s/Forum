using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace Forum.Api.Infrastructure.StartupConfigurations
{
    public class BanCheckMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;
        private readonly IDistributedCache _distributedCache;

        public BanCheckMiddleware(ILogger<ExceptionMiddleware> logger, 
            RequestDelegate requestDelegate, 
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
            _distributedCache = distributedCache;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity!.IsAuthenticated)
            {
                var user = httpContext.User;

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;

                if(!string.IsNullOrEmpty(userId))
                {
                    var isBanned = await _distributedCache.GetStringAsync($"is-banned:{userId}")
                        .ConfigureAwait(false);
                    if (isBanned != null)
                    {
                        httpContext.Response.StatusCode = 403;
                        await httpContext.Response.WriteAsync("User is banned.")
                            .ConfigureAwait(false);
                        return;
                    }
                }
                await _requestDelegate(httpContext);
            }
        }
    }
}
