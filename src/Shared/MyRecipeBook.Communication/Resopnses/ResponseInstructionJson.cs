using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Resopnses
{
    public class ResponseInstructionJson
    {
        public string Id { get; set; } = string.Empty;
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
