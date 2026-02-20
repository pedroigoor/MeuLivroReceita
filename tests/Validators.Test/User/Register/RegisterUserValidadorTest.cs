using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Excpitons;
using Shouldly;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidadorTest
    {
        [Fact]
        public void Sucesss()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();

        }

        [Fact]
        public void Error_Nome_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMenssagesException.NAME_EMPTY);

        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMenssagesException.EMAIL_EMPTY);

        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Email = "emai.com";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMenssagesException.EMAIL_INVALID);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);


            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMenssagesException.PASSWORD_EMPTY);

        }
    }
}
