using Bogus;
using CommonTestUtilities.Cryptography;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public static (User user, string password) Build()
        {

            var password = new Faker().Internet.Password();

            var user = new Faker<User>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => PasswordEncripter.Encrypt(password));

            return (user, password);
        }
    }
}
