using System.Diagnostics;
using BuberDiner.WebApi.Controllers;
using BuberDinner.Application.Services.Authentication;
using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Quries.Login;

using BuberDinner.Application.Services.Authentication.Common;

using BuberDinner.Contracts.Authentication;
using BuberDinner.WebApi.Filter;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace BuberDinner.WebApi.Controllers;

[ApiController]
[Route("auth")]

public class AuthenticationController : ApiController{

 private readonly ISender _mediator;
 private readonly IMapper _mapper;

 public AuthenticationController(ISender mediator, IMapper mapper){
    _mediator = mediator;
    _mapper = mapper;
 }
 [HttpPost("register")]
 public async Task<IActionResult> Register(RegisterRequest request){
    var command = _mapper.Map<RegisterCommand>(request); //new  RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
    return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));
 }

    private AuthenticationResponse NewMethod(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    }

    [HttpPost("login")]
 public async Task<IActionResult> Login(LoginRequest request){
     var command = _mapper.Map<LoginQuery>(request);//  new LoginQuery(request.Email, request.Password);
      ErrorOr<AuthenticationResult> authResult =await _mediator.Send(command);
     
     return authResult.Match(authResult=>Ok(NewMethod(authResult)),
     errors=>Problem(errors));
     
 }



}