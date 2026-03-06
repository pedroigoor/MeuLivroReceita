using Microsoft.AspNetCore.Http;
using MyRecipeBook.Communication.Request;

namespace MyRecipeBook.Application.UseCases.Recipe.Image
{
    public interface IAddUpdateImageCoverUseCase
    {
        Task Execute(long recipeId, IFormFile file);
    }
}
