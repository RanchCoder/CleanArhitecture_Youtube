using BuberDinner.Contracts.Authentication;
using BuberDinner.WebApi.Filter;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace BuberDiner.WebApi.Controllers;
[Route("Dinner")]
[Authorize]
public class DinnerController : ApiController{
 [HttpGet]
 public IActionResult ListDinners(){
    return Ok(Array.Empty<string>());
 }

}