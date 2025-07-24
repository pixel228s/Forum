using Forum.Application.Exceptions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Forum.Api.Infrastructure.StartupConfigurations
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "An application exception occurred.");
                await HandleApplicationExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleApplicationExceptionAsync(HttpContext httpContext, AppException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ex.Title,
                Status = ex.StatusCode,
                Detail = ex.Message,
            };
            return GetHttpResponse(problemDetails, httpContext);
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Internal server error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = ex.Message,
            };
            return GetHttpResponse(problemDetails, httpContext);
        }

        private static Task GetHttpResponse(ProblemDetails problemDetails, HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var result = JsonSerializer.Serialize(problemDetails, options);
            return httpContext.Response.WriteAsync(result);
        }
    }
}
