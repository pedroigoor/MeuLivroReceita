using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Infrastructe.DataAccess.Repositories
{
    public class UserRespository : IUserWriteOnlyRepository , IUserReadOnlyRepository
    {
        private readonly MyRecipeBookDbContext _context;
        public UserRespository(MyRecipeBookDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
           return  await _context.Users.AnyAsync(u => u.Email.Equals( email) && u.Active);
        }
    }
}
