using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependecyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
            AddPasswordEncrypt(services);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {

            services.AddScoped(options => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Services.AutoMapper.AutorMapping>();
            }).CreateMapper());
        }
        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase,RegisterUserUseCase>();
        }

        private static void AddPasswordEncrypt(this IServiceCollection services)
        {
            services.AddScoped(options => new PasswordEncripter());
        }
    }
}
