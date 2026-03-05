using FluentValidation;
using MyRecipeBook.Application.SharedValidators;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidador : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidador() { 
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(RuleFor => RuleFor.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(RuleFor => RuleFor.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(RuleFor => !string.IsNullOrEmpty(RuleFor.Email), () =>
            {
                RuleFor(RuleFor => RuleFor.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });


        }
    }
}
