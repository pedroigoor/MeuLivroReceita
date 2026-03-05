using MyRecipeBook.Communication.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Request
{
    public class RequestFilterRecipeJson
    {
        public string? RecipeTitle_Ingredient { get; set; }
        public IList<CookingTime> CookingTimes { get; set; } = [];
        public IList<Difficulty> Difficulties { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
    }
}
