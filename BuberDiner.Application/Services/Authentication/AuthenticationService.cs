using BuberDinner.Application.common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator){
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public AuthenticationResult Login(string email, string password)
    {
        
        return new AuthenticationResult(Guid.NewGuid(),"firstName","LastName",email, "token");
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, FirstName, LastName);
        return new AuthenticationResult(Guid.NewGuid(),FirstName,LastName,Email, token);
    }
}