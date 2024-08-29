using System.Security.Cryptography;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.WebApi.Controllers;

public class ApiController : ControllerBase{

    protected IActionResult Problem(List<Error> errors){
        HttpContext.Items.Add("errors",errors);
        var firstError = errors[0];
        var statusCode = firstError.Type switch{
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError 
        };

        return Problem(statusCode: statusCode, title : firstError.Description);
    }
}