using System.Diagnostics;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.WebApi.Filter;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebApi.Controllers;

[ApiController]
[Route("auth")]

public class AuthenticationController : ControllerBase{

 private readonly IAuthenticationService _authenticationService;
 public AuthenticationController(IAuthenticationService authenticationService){
    _authenticationService = authenticationService;
 }
 [HttpPost("register")]
 public IActionResult Register(RegisterRequest request){
    Debug.WriteLine(request.FirstName + " " + request.LastName);
    var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
    var response = new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    return Ok(response);
 }

 [HttpPost("login")]
 public IActionResult Login(LoginRequest request){
     var authResult = _authenticationService.Login(request.Email, request.Password);
     var response = new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    return Ok(response);
 }



}