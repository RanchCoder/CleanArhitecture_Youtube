using BuberDiner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BuberDiner.WebApi.Controllers;

public class ErrorsController : ControllerBase{

[Route("/errors")]
public IActionResult Error(){
      // to access exception 
      Exception? exception = HttpContext.Features.Get<ExceptionHandlerFeature>()?.Error;

      var (statusCode, message) = exception switch{
            IServiceException serviceException=> ((int)serviceException.StatusCode,serviceException.ErrorMessage),
           _=>(StatusCodes.Status500InternalServerError,exception?.GetType().Name)
      };

      return Problem(statusCode: statusCode, title : message);
}

}