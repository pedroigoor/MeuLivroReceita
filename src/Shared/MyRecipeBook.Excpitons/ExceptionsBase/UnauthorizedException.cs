using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class UnauthorizedException : MyRecipeBookException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
