using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.API.Filters;

namespace MyRecipeBook.API.Config
{
    public static class DependecyInjectionExtension
    {
        public static void AddApi(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

        }
    }
}
