using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityForm.Api.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestCultureMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            else if (context.Request.Headers.ContainsKey("Accept-Language"))
            {
                var cultureHeader = context.Request.Headers["Accept-Language"];
                if (cultureHeader.Any())
                {
                    var culture = new CultureInfo(cultureHeader.First().Split(',').First().Trim());

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
            }
            await _requestDelegate(context);
        }
    }
}