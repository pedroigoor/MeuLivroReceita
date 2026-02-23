using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);

        public Task<Entities.User?> GetByEmailAndPass(string email,string passWord);
    }
}
