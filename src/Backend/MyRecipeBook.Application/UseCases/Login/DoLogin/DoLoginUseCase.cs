using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Cryptogaphy;
using MyRecipeBook.Excpitons.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
     
    public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
                                IAccessTokenGenerator accessTokenGenerator,
                                IPasswordEncripter passwordEncripter) : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
        private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {   
            
            var user = await _userReadOnlyRepository
                              .GetByEmailAndPass(request.Email,
                              _passwordEncripter.Encrypt(request.PassWord)) ?? throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.GenerateToken(user.UserIdentifier)
                }
            };
        }
    }
}
