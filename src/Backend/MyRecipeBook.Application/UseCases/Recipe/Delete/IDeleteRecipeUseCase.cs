using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete
{
    public interface IDeleteRecipeUseCase
    {
        Task Execute(long recipeId);
    }
}
