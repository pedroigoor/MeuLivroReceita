using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate
{
    public interface IGenerateRecipeUseCase
    {
        Task<ResponseGeneratedRecipeJson> Execute(RequestGenerateRecipeJson request);
    }
}
