using System.Security.Cryptography.X509Certificates;
using BuberDiner.Application.Common.Errors;
using BuberDiner.Domain.Entities;
using BuberDiner.Domain.Errors;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user){
            return Errors.InvalidCredentials.InvalidCredential;
        }
        if(user.Password != password){
            return Errors.InvalidCredentials.InvalidCredential;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password)
    {

       if(_userRepository.GetUserByEmail(Email) != null){
         return Errors.User.DuplicateEmail;
       }
       var user = new User{
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        Password = Password
       };

       _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}