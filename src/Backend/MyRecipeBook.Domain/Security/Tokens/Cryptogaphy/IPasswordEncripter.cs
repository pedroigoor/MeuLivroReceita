namespace MyRecipeBook.Domain.Security.Tokens.Cryptogaphy
{
    public interface IPasswordEncripter
    {

         public string Encrypt(string password);
    }
}
