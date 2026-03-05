using AutoMapper;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Security.Tokens.Cryptogaphy;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, 
                                   IUserReadOnlyRepository userReadOnlyRepository,
                                   IMapper mapper,
                                   IUnitOfWork unitOfWork,
                                   IAccessTokenGenerator accessTokenGenerator,
                                   IPasswordEncripter passwordEncripter)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           _accessTokenGenerator = accessTokenGenerator;
           _passwordEncripter = passwordEncripter;

        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {   
           await ValidateRequest(request);
            

           var user = _mapper.Map<Domain.Entities.User>(request);
           user.Password = _passwordEncripter.Encrypt(request.Password);
           user.UserIdentifier = Guid.NewGuid();

           await _userWriteOnlyRepository.Add(user);
           await _unitOfWork.Commit();
           return  new ResponseRegisteredUserJson { 
               Name = user.Name,
               Tokens = new ResponseTokensJson
               {
                   AccessToken = _accessTokenGenerator.GenerateToken(user.UserIdentifier)
               }
           };
        }


        private async Task ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExist) {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED)); 
            }

            if (!result.IsValid )
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
