using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.UseCases.Recipe.Register
{
    public interface IRegisterRecipeUseCase
    {
        public Task<ResponseRegiteredRecipeJson> Execute(RequestRegisterRecipeFormData request);
    }
}
