using MyRecipeBook.API.Middleware;

namespace MyRecipeBook.API.Config
{
    public static class MiddlewaresConfig
    {
        public static void RegisterMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<CultureMiddleware>();
        }
    }
}
