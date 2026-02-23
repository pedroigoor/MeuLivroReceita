using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
