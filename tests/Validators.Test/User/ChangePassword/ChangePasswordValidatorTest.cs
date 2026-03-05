using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Excpitons;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Validators.Test.User.ChangePassword
{

    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.PASSWORD_EMPTY);
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordJsonBuilder.Build();
            request.NewPassword = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.PASSWORD_EMPTY);
        }
    }
}
