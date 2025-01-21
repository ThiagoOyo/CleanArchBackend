using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CleanArch.WebAPI.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        private const string ApiKeyHeaderName = "X-API-KEY";
        private readonly string _configuredApiKey = Environment.GetEnvironmentVariable("API_KEY") 
                                                    ?? throw new InvalidOperationException("API Key not configured.");

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key is missing.");
                return;
            }

            if (!_configuredApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }

            await next(context);
        }
    }
}