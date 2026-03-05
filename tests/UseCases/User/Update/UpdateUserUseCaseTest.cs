using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases.Test.User.Update
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => await useCase.Execute(request);

            await act.ShouldNotThrowAsync();

            user.Name.ShouldBe(request.Name);
            user.Email.ShouldBe(request.Email);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase(user);

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
               () => useCase.Execute(request)
           );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                .ShouldContain(ResourceMessagesException.NAME_EMPTY);


            user.Name.ShouldNotBe(request.Name);
            user.Email.ShouldNotBe(request.Email);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Email);

           

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                     () => useCase.Execute(request)
                 );

            exception.Errors.Count.ShouldBe(1);
            exception.Errors
                .ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);

            user.Name.ShouldNotBe(request.Name);
            user.Email.ShouldNotBe(request.Email);
        }

        private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, string? email = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if (string.IsNullOrEmpty(email) == false)
                userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new UpdateUserUseCase(loggedUser, userUpdateRepository, userReadOnlyRepositoryBuilder.Build(), unitOfWork);
        }
    }
}
