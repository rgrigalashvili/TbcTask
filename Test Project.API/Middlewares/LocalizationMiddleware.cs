using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project.API.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var currentCulture = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            var defaultCulture = "en";

            if (!string.IsNullOrWhiteSpace(currentCulture))
            {
                var checkCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).Any(p => string.Equals(p.Name, currentCulture, StringComparison.CurrentCultureIgnoreCase));
                if (!checkCulture)
                {
                    currentCulture = defaultCulture;
                }
            }
            else
            {
                currentCulture = defaultCulture;
            }

            var cultureInfo = new CultureInfo(currentCulture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
