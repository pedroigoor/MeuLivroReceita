using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructe.DataAccess;
using MyRecipeBook.Infrastructe.DataAccess.Repositories;
using MyRecipeBook.Infrastructe.Extensions;

namespace MyRecipeBook.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbRepository(services);

            if (configuration.IsUnitTestEnviroment())
            {
              return;
            }

            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyRecipeBookDbContext>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString());
            });
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
                services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(DependecyInjectionExtension).Assembly).For.All())
                    .AddLogging(lb => lb.AddFluentMigratorConsole());


        }
        private static void AddDbRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository,UserRespository>();
            services.AddScoped<IUserReadOnlyRepository, UserRespository>();
        }


    }
}
