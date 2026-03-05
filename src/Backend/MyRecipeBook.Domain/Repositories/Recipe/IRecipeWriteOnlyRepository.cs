using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Repositories.Recipe
{
    public interface IRecipeWriteOnlyRepository
    {
        Task Add(Entities.Recipe recipe);
        Task Delete(long recipeId);
    }
}
