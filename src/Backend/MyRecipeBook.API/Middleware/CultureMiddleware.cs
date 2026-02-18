using System.Globalization;

namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var cultureQuery = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var culture = new CultureInfo("en");


            if (!string.IsNullOrWhiteSpace(cultureQuery)  && 
                supportedCultures.Any(c => c.Name.Equals(cultureQuery)))
            {
                culture = new CultureInfo(cultureQuery);
              
            }

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            await _next(context);
        }
    }
}
