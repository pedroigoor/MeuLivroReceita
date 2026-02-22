using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
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

        public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, 
                                   IUserReadOnlyRepository userReadOnlyRepository,
                                   IMapper mapper,
                                   IUnitOfWork unitOfWork)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {   
           await ValidateRequest(request);
            

           var user = _mapper.Map<Domain.Entities.User>(request);

           user.Password = PasswordEncripter.Encrypt(request.Password);

           await _userWriteOnlyRepository.Add(user);
           await _unitOfWork.Commit();
            return  new ResponseRegisteredUserJson { Name = user.Name };
        }


        private async Task ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExist) {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMenssagesException.EMAIL_ALREADY_REGISTERED)); 
            }

            if (!result.IsValid )
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
