using System.Security.Cryptography.X509Certificates;
using BuberDiner.Application.Common.Errors;
using BuberDiner.Domain.Entities;
using BuberDiner.Domain.Errors;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using ErrorOr;
using BuberDinner.Application.Services.Authentication.Common;

namespace BuberDinner.Application.Services.Authentication.Command;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
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