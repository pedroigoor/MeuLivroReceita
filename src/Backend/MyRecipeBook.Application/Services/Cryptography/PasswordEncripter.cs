using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        public static string Encrypt(string password)
        {
            // Implementação de criptografia de senha (exemplo usando SHA256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
