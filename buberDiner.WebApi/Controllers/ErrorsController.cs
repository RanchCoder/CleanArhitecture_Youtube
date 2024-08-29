using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BuberDiner.WebApi.Controllers;

public class ErrorsController : ControllerBase{

[Route("/errors")]
public IActionResult Error(){
      // to access exception 
      Exception? exception = HttpContext.Features.Get<ExceptionHandlerFeature>()?.Error;
      return Problem(title : exception?.Message , statusCode : 400);
}

}