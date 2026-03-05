using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.ChangePassword
{

    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();

            var request = RequestChangePasswordJsonBuilder.Build();
            request.Password = password;

            var useCase = CreateUseCase(user);

            await Should.NotThrowAsync(() => useCase.Execute(request));
        }

        [Fact]
        public async Task Error_NewPassword_Empty()
        {
            (var user, var password) = UserBuilder.Build();

            var request = new RequestChangePasswordJson
            {
                Password = password,
                NewPassword = string.Empty
            };

            var useCase = CreateUseCase(user);

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                 .ShouldContain(ResourceMessagesException.PASSWORD_EMPTY);
        }

        [Fact]
        public async Task Error_CurrentPassword_Different()
        {
            (var user, var password) = UserBuilder.Build();

            var request = RequestChangePasswordJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                     .ShouldContain(ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD);
        }

        private static ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var passwordEncripter = PasswordEncripterBuilder.Build();

            return new ChangePasswordUseCase(loggedUser, passwordEncripter, userUpdateRepository, unitOfWork);
        }
    }
}
