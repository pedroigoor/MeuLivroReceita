using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class RefreshTokenExpiredException : MyRecipeBookException
    {
        public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
