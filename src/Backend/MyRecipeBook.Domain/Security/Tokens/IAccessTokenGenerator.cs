using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Domain.Security.Tokens
{
    public interface  IAccessTokenGenerator
    {
        public string GenerateToken(Guid userIntentifier);
    }
}
