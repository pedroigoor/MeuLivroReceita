using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Excpitons;
using MyRecipeBook.Excpitons.ExceptionsBase;
using System;
using System.Net;

namespace MyRecipeBook.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyRecipeBookException)
            {
                HandleProjectException(context);
            }
            else {
                ThrowUnknowException(context);
            }
        }

        public static void HandleProjectException(ExceptionContext contex)
        {
            if(contex.Exception is ErrorOnValidationException)
            {
                var excepiton = contex.Exception as ErrorOnValidationException;

                contex.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                contex.Result = new BadRequestObjectResult( new ResponseErrorJson(excepiton!.Errors));
            }
        }

        public static void ThrowUnknowException(ExceptionContext contex)
        {
            contex.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            contex.Result = new ObjectResult(new ResponseErrorJson(ResourceMenssagesException.UNKNOW_ERROR));
        }
    }

   
}
