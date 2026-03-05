using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using WebApi.test.InlineData;

namespace WebApi.test.User.ChangePassword
{

    public class ChangePasswordTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "user/change-password";

        private readonly string _password;
        private readonly string _email;
        private readonly Guid _userIdentifier;

        public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _password = factory.GetPassword();
            _email = factory.GetEmail();
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestChangePasswordJsonBuilder.Build();
            request.Password = _password;

            var token = JwtTokenGenaratorBuilder.Build().GenerateToken(_userIdentifier);

            var response = await DoPut(METHOD, request, token);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var loginRequest = new RequestLoginJson
            {
                Email = _email,
                PassWord = _password,
            };

            response = await DoPost(method: "login", request: loginRequest);
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            loginRequest.PassWord = request.NewPassword;

            response = await DoPost(method: "login", request: loginRequest);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_NewPassword_Empty(string culture)
        {
            var request = new RequestChangePasswordJson
            {
                Password = _password,
                NewPassword = string.Empty
            };

            var token = JwtTokenGenaratorBuilder.Build().GenerateToken(_userIdentifier);

            var response = await DoPut(METHOD, request, token, culture);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement
                                     .GetProperty("errors")
                                     .EnumerateArray()
                                     .Select(e => e.GetString())
                                     .ToList();

            var expectedMessage =   ResourceMessagesException.ResourceManager
                .GetString("PASSWORD_EMPTY", new CultureInfo(culture));

            errors.Count.ShouldBe(1);
            errors.ShouldContain(expectedMessage);
        }
    }
}
