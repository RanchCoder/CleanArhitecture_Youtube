using System.Security.Cryptography;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDiner.WebApi.Controllers;

public class ApiController : ControllerBase{

    protected IActionResult Problem(List<Error> errors){

       if(errors.All(error=> error.Type == ErrorType.Validation)){
          return ValidationProblem(errors);
       }


        HttpContext.Items.Add("errors",errors);
        var firstError = errors[0];
        return Problem(firstError);
    }

    private IActionResult Problem(Error firstError)
    {
        var statusCode = firstError.Type switch{
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError 
        };

        return Problem(statusCode: statusCode, title : firstError.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateErrorDictionary = new ModelStateDictionary();
          foreach(var error in errors){
                modelStateErrorDictionary.AddModelError(error.Code, error.Description);
          }
          return ValidationProblem(modelStateErrorDictionary);
    }
}