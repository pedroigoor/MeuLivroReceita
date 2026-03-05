using MyRecipeBook.Communication.Resopnses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.Dashboard
{
    public interface IGetDashboardUseCase
    {
        Task<ResponseRecipesJson> Execute();
    }
}
