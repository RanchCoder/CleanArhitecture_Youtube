using System.Diagnostics;
using BuberDiner.WebApi.Controllers;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Services.Authentication.Command;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Query;
using BuberDinner.Contracts.Authentication;
using BuberDinner.WebApi.Filter;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

[ApiController]
[Route("auth")]

public class AuthenticationController : ApiController{

 private readonly IAuthenticationCommandService _authenticationCommandService;
  private readonly IAuthenticationQueryService _authenticationQueryService;
 public AuthenticationController(IAuthenticationCommandService authenticationCommandService,IAuthenticationQueryService authenticationQueryService){
    _authenticationCommandService = authenticationCommandService;
    _authenticationQueryService = authenticationQueryService;
 }
 [HttpPost("register")]
 public IActionResult Register(RegisterRequest request){
    Debug.WriteLine(request.FirstName + " " + request.LastName);
    ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
    return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));


 }

    private AuthenticationResponse NewMethod(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    }

    [HttpPost("login")]
 public IActionResult Login(LoginRequest request){
     ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(request.Email, request.Password);
     return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));
     
 }



}