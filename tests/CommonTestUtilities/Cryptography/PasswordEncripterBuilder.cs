using MyRecipeBook.Domain.Security.Cryptogaphy;
using MyRecipeBook.Infrastructe.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter();
    }
}
