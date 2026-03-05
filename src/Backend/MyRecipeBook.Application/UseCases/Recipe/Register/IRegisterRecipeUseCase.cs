using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.Recipe.Register
{
    public interface IRegisterRecipeUseCase
    {
        public Task<ResponseRegiteredRecipeJson> Execute(RequestRecipeJson request);
    }
}
