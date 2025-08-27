using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace Forum.Web.Helper.Middlewares
{
    public class CheckBanWebMiddleware
    {
        private readonly ILogger<CheckBanWebMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;
        private readonly IDistributedCache _distributedCache;

        public CheckBanWebMiddleware(ILogger<CheckBanWebMiddleware> logger,
            RequestDelegate requestDelegate,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
            _distributedCache = distributedCache;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/Home/Banned") ||
                httpContext.Request.Path.StartsWithSegments("/Account/Logout"))
            {
                await _requestDelegate(httpContext);
                return;
            }

            if (httpContext.User.Identity!.IsAuthenticated)
            {
                var user = httpContext.User;
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;

                if (!string.IsNullOrEmpty(userId))
                {
                    var isBanned = await _distributedCache.GetStringAsync($"is-banned:{userId}")
                        .ConfigureAwait(false);

                    if (isBanned != null)
                    {
                        httpContext.Session.SetString("BanMessage", "Your account has been banned.");
                        httpContext.Response.Redirect("/Home/Banned");
                        return; 
                    }
                }
            }
            await _requestDelegate(httpContext);
        }
    }
}
