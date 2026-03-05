using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Request
{
    public class RequestInstructionJson
    {
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
