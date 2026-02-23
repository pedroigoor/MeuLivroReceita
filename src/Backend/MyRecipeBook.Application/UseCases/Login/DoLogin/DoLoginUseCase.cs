using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Excpitons.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin
{
     
    public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository) : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {   
            
            var user = await _userReadOnlyRepository
                              .GetByEmailAndPass(request.Email, 
                              PasswordEncripter.Encrypt(request.PassWord)) ?? throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name
            };
        }
    }
}
