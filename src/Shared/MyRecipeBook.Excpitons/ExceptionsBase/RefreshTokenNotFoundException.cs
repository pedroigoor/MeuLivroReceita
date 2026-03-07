using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class RefreshTokenNotFoundException : MyRecipeBookException
    {
        public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
