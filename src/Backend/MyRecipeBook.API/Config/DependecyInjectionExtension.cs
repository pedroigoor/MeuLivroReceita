using MyRecipeBook.API.Filters;
using MyRecipeBook.API.Token;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.API.Config
{
    public static class DependecyInjectionExtension
    {
        public static void AddApi(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
            services.AddScoped<ITokenProvider, HttpContextTokenValue>();

        }
    }
}
