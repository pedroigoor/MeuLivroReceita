

using MyRecipeBook.Domain.Security.Cryptogaphy;

namespace MyRecipeBook.Infrastructe.Security.Cryptography
{
    public class BCryptNet : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsValid(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
