using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Excpitons;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebApi.test.InlineData;

namespace WebApi.test.Login.DoLogin
{
    public class DoLoginTest : MyRecipeBookClassFixture
    {
        private readonly string method = "login";

        private readonly string _email;
        private readonly string _password;
        private readonly string _name;

        public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
        {
            
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _name = factory.GetName();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                PassWord = _password
            };

            var response = await DoPost(method, request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            await using var responseBody =
                await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);

            var responseData =
                await JsonDocument.ParseAsync(responseBody,
                    cancellationToken: TestContext.Current.CancellationToken);

            var name = responseData
                .RootElement
                .GetProperty("name")
                .GetString();

            name.ShouldNotBeNullOrWhiteSpace();
            name.ShouldBe(_name);

            var accessToken = responseData
                .RootElement
                .GetProperty("tokens")
                .GetProperty("accessToken")
                .GetString();

            accessToken.ShouldNotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginJsonBuilder.Build();
            
            var response = await DoPost(method, request, culture);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            await using var responseBody = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
            var responseData = await JsonDocument.ParseAsync(responseBody, cancellationToken: TestContext.Current.CancellationToken);

            var errors = responseData
                .RootElement
                .GetProperty("errors")
                .EnumerateArray();

            var expectedMessage = ResourceMenssagesException
                .ResourceManager
                .GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            errors.Count().ShouldBe(1);
            errors.ShouldContain(errors => errors.GetString()!.Equals(expectedMessage));
        }
    }
}
