using FinApp.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FinApp.Extensions
{
    public static class MiddlewareConfiguration
    {
        // Extension method used to add the middleware to the HTTP request pipeline.
        public static IApplicationBuilder UseGlobalExcepionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExcepionMiddleware>();
        }
    }
}
