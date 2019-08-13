using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace FinApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExcepionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExcepionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<GlobalExcepionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ApiException e)
            {
                logger.LogDebug(e, "API Exception");
                httpContext.Response.StatusCode = (int) e.StatusCode;
                if (!String.IsNullOrEmpty(e.Message))
                {
                    var body = JsonConvert.SerializeObject(new {Error = e.Message});
                    var bytes = Encoding.UTF8.GetBytes(body);
                    await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }

            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception");
                httpContext.Response.StatusCode = 500;
      
                var body = JsonConvert.SerializeObject(new { e.Message });
                var bytes = Encoding.UTF8.GetBytes(body);
                await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExcepionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExcepionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExcepionMiddleware>();
        }
    }
}
