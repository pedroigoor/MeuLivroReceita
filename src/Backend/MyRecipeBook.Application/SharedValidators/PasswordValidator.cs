using FluentValidation;
using FluentValidation.Validators;
using MyRecipeBook.Excpitons;

namespace MyRecipeBook.Application.SharedValidators
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "PasswordValidator";

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMenssage", ResourceMessagesException.PASSWORD_EMPTY);
                return false;

            }

            if (password.Length < 6)
            {
                context.MessageFormatter.AppendArgument("ErrorMenssage", ResourceMessagesException.PASSWORD_EMPTY);
                return false;

            }
            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{ErrorMenssage}";
        }
    }
}
