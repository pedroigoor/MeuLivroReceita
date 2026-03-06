using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.UseCases.Dashboard;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.Recipe.Delete;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Application.UseCases.Recipe.Generate;
using MyRecipeBook.Application.UseCases.Recipe.GetById;
using MyRecipeBook.Application.UseCases.Recipe.Image;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.Recipe.Update;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using Sqids;

namespace MyRecipeBook.Application
{
    public static class DependecyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            AddUseCases(services);
            AddAutoMapper(services);
            AddIdEncoder(services, configuration);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
           
            services.AddScoped(options => new AutoMapper.MapperConfiguration(autorMapperOpt =>
            {
                var sqids = options.GetRequiredService<SqidsEncoder<long>>();
                autorMapperOpt.AddProfile(new AutorMapping(sqids));
            }).CreateMapper());
        }
        private static void AddIdEncoder(this IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                Alphabet = configuration.GetValue<string>("Settings:IdCryptAlphabet")!,
                MinLength = 3,
            });
            services.AddSingleton(sqids);
        }
        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase,RegisterUserUseCase>();         
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
            services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
            services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();
            services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
            services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
            services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();
            services.AddScoped<IGenerateRecipeUseCase, GenerateRecipeUseCase>();
            services.AddScoped<IAddUpdateImageCoverUseCase, AddUpdateImageCoverUseCase>();






        }

       
    }
}
