using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyRecipeBook.Domain.Entities
{
    [Table("Ingredients")]
    public class Ingredient : EntityBase
    {
        public string Item { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
