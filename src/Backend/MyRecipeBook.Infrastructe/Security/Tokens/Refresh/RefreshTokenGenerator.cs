using MyRecipeBook.Domain.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Infrastructe.Security.Tokens.Refresh
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
