using MyRecipeBook.Domain.Security.Tokens.Cryptogaphy;
using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Infrastructe.Security.Cryptography
{
    public class Sha512Encripter : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);

        }
    }
}
