using MinimalArchitecture.Common.Models;
using MinimalArchitecture.Common.Results;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Runtime.Versioning;
using MinimalArchitecture.Common.Extensions;

namespace MinimalArchitecture.Api.Middlewares
{
    public class CatchErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<CatchErrorMiddleware> _logger;

        public CatchErrorMiddleware(RequestDelegate next,
                                    IHttpContextAccessor httpContext,
                                    ILogger<CatchErrorMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _httpContext = httpContext;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error no controlado");
                var error = Result.Fail(Common.Errors.Common.UNEXPECTED_ERROR());
                context.Response.ContentType = Common.Enums.ContentType.JSON;
                _ = context.Response.WriteAsync(error.ToJson());
                
            }
            

           
        }
    }
}
