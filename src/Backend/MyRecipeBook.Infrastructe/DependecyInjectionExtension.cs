using Azure.Storage.Blobs;
using FirebirdSql.Data.Services;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Cryptogaphy;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.OpenAI;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Infrastructe.DataAccess;
using MyRecipeBook.Infrastructe.DataAccess.Repositories;
using MyRecipeBook.Infrastructe.Extensions;
using MyRecipeBook.Infrastructe.Security.Cryptography;
using MyRecipeBook.Infrastructe.Security.Tokens.Access.Generator;
using MyRecipeBook.Infrastructe.Security.Tokens.Access.Validator;
using MyRecipeBook.Infrastructe.Services;
using MyRecipeBook.Infrastructe.Services.OpenAi;
using MyRecipeBook.Infrastructe.Services.Storage;
using OpenAI.Chat;

namespace MyRecipeBook.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbRepository(services);
            AddLoggedUser(services);
            AddToken(services, configuration);
            AddPasswordEncrypt(services);
            AddChatGptService(services, configuration);
            AddAzureStorage(services, configuration);
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
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

            //services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();
            services.AddScoped<IRecipeWriteOnlyRepository, RecipeRepository>();
            services.AddScoped<IRecipeReadOnlyRepository, RecipeRepository>();
            services.AddScoped<IRecipeUpdateOnlyRepository, RecipeRepository>();
            //services.AddScoped<ITokenRepository, TokenRepository>();


        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();


        private static void AddPasswordEncrypt(this IServiceCollection services)
        {
            services.AddScoped<IPasswordEncripter>(options => new Sha512Encripter());
        }
        private static void AddChatGptService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenerateRecipeAI, ChatGptService>();

            var apiKey = configuration.GetValue<string>("Settings:OpenAI:Apikey");

            services.AddScoped(c => new ChatClient(MyRecipeBookRuleConstants.CHAT_MODEL, apiKey));
        }

        private static void AddAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Settings:BlobStorage:Azure");
            services.AddScoped<IBlobStorageService>( c => new AzureStorageService ( new BlobServiceClient(connectionString)));

        }
    }
}
