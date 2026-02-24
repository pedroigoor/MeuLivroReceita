using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Security.Tokens
{
    public interface IAccessTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
    }
}
