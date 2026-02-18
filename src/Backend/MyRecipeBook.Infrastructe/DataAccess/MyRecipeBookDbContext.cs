using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructe.DataAccess
{
    public class MyRecipeBookDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public MyRecipeBookDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
        }
    }
}
