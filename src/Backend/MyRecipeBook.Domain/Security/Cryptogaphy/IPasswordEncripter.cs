namespace MyRecipeBook.Domain.Security.Cryptogaphy
{
    public interface IPasswordEncripter
    {

         public string Encrypt(string password);
         public bool IsValid(string password, string passwordHash);
    }
}
