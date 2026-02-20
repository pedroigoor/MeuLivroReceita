using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;
using Shouldly;

namespace UseCases.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Sucesss()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
         }


        [Fact]
        public async Task Error_Email_Already_Registred()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                .ShouldContain(ResourceMenssagesException.EMAIL_ALREADY_REGISTERED);
        }
        [Fact]
        public async Task Error_Name_Empty()
        {

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty; 

            var useCase = CreateUseCase();

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                .ShouldContain(ResourceMenssagesException.NAME_EMPTY);
        }

        private RegisterUserUseCase CreateUseCase(string? email =null )
        {

            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if (string.IsNullOrEmpty(email) == false)
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), mapper, passwordEncripter, unitOfWork);

        }
    }
}
