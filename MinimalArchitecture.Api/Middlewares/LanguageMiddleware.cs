using System.Globalization;

namespace MinimalArchitecture.Api.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContext;

        public LanguageMiddleware(RequestDelegate next,IHttpContextAccessor httpContext)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _httpContext = httpContext;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Headers.ContainsKey("accept-language"))
                {
                    var language = context.Request.Headers["accept-language"].ToString();
                    CultureInfo.CurrentCulture = new CultureInfo(language);
                    CultureInfo.CurrentUICulture = new CultureInfo(language);
                }

            }
            catch (Exception ex)
            {

                _ = ex;
            }
            
            await _next(context);

           
        }
    }
}
