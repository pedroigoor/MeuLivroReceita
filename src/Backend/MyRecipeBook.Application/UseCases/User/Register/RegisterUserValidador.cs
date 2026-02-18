using FluentValidation;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidador : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidador() { 
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMenssagesException.NAME_EMPTY);
            RuleFor(RuleFor => RuleFor.Email).NotEmpty().WithMessage(ResourceMenssagesException.EMAIL_EMPTY);
            RuleFor(RuleFor => RuleFor.Email).EmailAddress().WithMessage(ResourceMenssagesException.EMAIL_INVALID);
            RuleFor(RuleFor => RuleFor.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMenssagesException.PASSWORD_EMPTY);


        }
    }
}
