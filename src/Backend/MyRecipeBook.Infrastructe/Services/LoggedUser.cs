using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Infrastructe.DataAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyRecipeBook.Infrastructe.Services
{
    public class LoggedUser : ILoggedUser
    {
        private readonly MyRecipeBookDbContext _context;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(MyRecipeBookDbContext context,
                          ITokenProvider tokenProvider)
        {
            _context = context;
            _tokenProvider = tokenProvider;
        }
        public async Task<User> User()
        {
            var tokenProvider = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(tokenProvider);

           var identifier = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

           var userIdentifier = Guid.Parse(identifier);

            return await _context
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);

        }
    }
}
