using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyRecipeBook.Infrastructe.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _secretKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string secretKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _secretKey = secretKey;
        }
        public string GenerateToken(Guid userIntentifier)
        {
            var tokenDescripitor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                 {
                    new Claim(ClaimTypes.Sid, userIntentifier.ToString())
                })
            };
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripitor);
            return tokenHandler.WriteToken(token);

        }
    }


}
