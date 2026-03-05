using MyRecipeBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Dtos
{
    public record FilterRecipesDto
    {
        public string? RecipeTitle_Ingredient { get; init; }
        public IList<CookingTime> CookingTimes { get; init; } = [];
        public IList<Difficulty> Difficulties { get; init; } = [];
        public IList<DishType> DishTypes { get; init; } = [];
    }
}
