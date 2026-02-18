using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructe.DataAccess;
using MyRecipeBook.Infrastructe.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        //public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)


        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbRepository(services);
            AddDbContext(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyRecipeBookDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Connection"));
            });
        }
        private static void AddDbRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository,UserRespository>();
            services.AddScoped<IUserReadOnlyRepository, UserRespository>();
        }


    }
}
