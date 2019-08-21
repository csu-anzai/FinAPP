using BLL.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                logger.LogDebug(e, nameof(ApiException));
                await FillUpExceptionMessage(e, new { error = e.Message });
            }
            catch (ValidationExeption e)
            {
                await FillUpExceptionMessage(e, new { code = e.ValidationErrorCode, error = e.Message, parameter = e.Parameter });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (!String.IsNullOrEmpty(e.Message))
                {
                    var body = JsonConvert.SerializeObject(e.Message);
                    var bytes = Encoding.UTF8.GetBytes(body);
                    await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            async Task FillUpExceptionMessage(CustomExeption exception, object jsonObj)
            {
                httpContext.Response.StatusCode = (int)exception.Code;

                if (!String.IsNullOrEmpty(exception.Message))
                {
                    var body = JsonConvert.SerializeObject(jsonObj);
                    var bytes = Encoding.UTF8.GetBytes(body);
                    await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
