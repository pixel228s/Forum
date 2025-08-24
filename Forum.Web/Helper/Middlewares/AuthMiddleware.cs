namespace Forum.Web.Helper.Middlewares
{
    public static class AuthMiddleware
    {
        public static void UseJwtMiddleware(this WebApplication app) 
        {
            app.Use(async (context, next) =>
            {
                var jwt = context.Request.Cookies["jwt"];
                if (!string.IsNullOrEmpty(jwt) && !context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Append("Authorization", $"Bearer {jwt}");
                }
                await next();
            });
        }               
    }
}
