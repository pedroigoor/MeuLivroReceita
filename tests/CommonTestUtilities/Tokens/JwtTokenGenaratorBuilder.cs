using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructe.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public class JwtTokenGenaratorBuilder
    {
        public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, secretKey: "MinhaChaveSecretaSuperSeguraaaaaaaaaaaaa");
    }
}
