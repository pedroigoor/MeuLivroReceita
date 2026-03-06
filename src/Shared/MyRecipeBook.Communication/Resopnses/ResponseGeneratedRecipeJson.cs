using MyRecipeBook.Communication.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Resopnses
{
    public class ResponseGeneratedRecipeJson
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<ResponseGeneratedInstructionJson> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
