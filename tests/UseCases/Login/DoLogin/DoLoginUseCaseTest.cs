using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success() { 

            (var user,var pass) = UserBuilder.Build();
            var usecase = CreateUseCase(user); 

            var result = await usecase.Execute( new RequestLoginJson
                                               { Email = user.Email, 
                                                 PassWord = pass });

            result.ShouldNotBeNull();
            result.Tokens.ShouldNotBeNull();
            result.Name.ShouldNotBeNullOrWhiteSpace(user.Name);
            result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();


        }


        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase();

            var exception = await Should.ThrowAsync<InvalidLoginException>(
                () => useCase.Execute(request)
            );

            exception.Message.ShouldBe(ResourceMenssagesException.EMAIL_OR_PASSWORD_INVALID);

        }


        private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user  = null) {

            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var accessToken =  JwtTokenGenaratorBuilder.Build();

            if (user is not null)
            {
                readRepositoryBuilder.GetByEmailAndPass(user);
            }

            return new DoLoginUseCase(readRepositoryBuilder.Build(), accessToken);

        }
    }
}
