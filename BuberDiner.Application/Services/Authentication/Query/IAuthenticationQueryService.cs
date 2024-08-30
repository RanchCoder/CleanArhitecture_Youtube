using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Query;

public interface IAuthenticationQueryService{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    //ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);
}