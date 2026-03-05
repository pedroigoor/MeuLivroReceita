using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById
{
    public interface IGetRecipeByIdUseCase
    {
        Task<ResponseRecipeJson> Execute(long recipeId);
    }
}
