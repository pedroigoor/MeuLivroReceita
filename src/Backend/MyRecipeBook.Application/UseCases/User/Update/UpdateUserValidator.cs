using FluentValidation;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMenssagesException.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMenssagesException.EMAIL_EMPTY);

            When(request =>!string.IsNullOrWhiteSpace(request.Email), () =>
            {
                RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMenssagesException.EMAIL_INVALID);
            });
        }
    }
}
