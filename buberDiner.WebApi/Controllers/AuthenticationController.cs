using System.Diagnostics;
using BuberDiner.WebApi.Controllers;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.WebApi.Filter;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

[ApiController]
[Route("auth")]

public class AuthenticationController : ApiController{

 private readonly IAuthenticationService _authenticationService;
 public AuthenticationController(IAuthenticationService authenticationService){
    _authenticationService = authenticationService;
 }
 [HttpPost("register")]
 public IActionResult Register(RegisterRequest request){
    Debug.WriteLine(request.FirstName + " " + request.LastName);
    ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
    return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));


 }

    private AuthenticationResponse NewMethod(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    }

    [HttpPost("login")]
 public IActionResult Login(LoginRequest request){
     ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(request.Email, request.Password);
     return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));
     
 }



}