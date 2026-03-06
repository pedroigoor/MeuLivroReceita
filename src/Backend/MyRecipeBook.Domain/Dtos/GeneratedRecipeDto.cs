using MyRecipeBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Dtos
{
    public record GeneratedRecipeDto
    {
        public string Title { get; init; } = string.Empty;
        public IList<string> Ingredients { get; init; } = [];
        public IList<GeneratedInstructionDto> Instructions { get; init; } = [];
        public CookingTime CookingTime { get; init; }
    }
}
