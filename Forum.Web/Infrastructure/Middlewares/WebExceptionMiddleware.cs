using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Forum.Web.Helper.Middlewares
{
    public class WebExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public WebExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
          
            httpContext.Session.Set("ErrorMessage", Encoding.UTF8.GetBytes(ex.Message));

            httpContext.Response.Clear();
            httpContext.Response.Redirect("/Home/Error");
        }
    }

}

