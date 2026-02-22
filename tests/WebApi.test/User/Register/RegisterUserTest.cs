using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using MyRecipeBook.Excpitons;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.test.InlineData;
using Xunit.Sdk;

namespace WebApi.test.User.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFacotory>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(CustomWebApplicationFacotory factory) => _httpClient = factory.CreateClient();

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var response = await _httpClient.PostAsJsonAsync("User", request);

            response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace();

            responseData.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);


        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
             var request = RequestRegisterUserJsonBuilder.Build();
             request.Name = string.Empty;
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
            var response = await _httpClient.PostAsJsonAsync("User", request);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData
                .RootElement
                .GetProperty("errors")
                .EnumerateArray();

            var expectedMessage = ResourceMenssagesException
                .ResourceManager
                .GetString("NAME_EMPTY", new CultureInfo(culture));

            errors.Count().ShouldBe(1);
            errors.ShouldContain(errors => errors.GetString()!.Equals(expectedMessage));
        }

    }
}
