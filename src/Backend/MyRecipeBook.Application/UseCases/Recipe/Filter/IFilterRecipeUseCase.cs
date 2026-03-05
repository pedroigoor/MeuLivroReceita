using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter
{
    public interface IFilterRecipeUseCase
    {
        Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request);
    }
}
