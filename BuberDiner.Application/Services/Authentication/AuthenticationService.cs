using System.Security.Cryptography.X509Certificates;
using BuberDiner.Domain.Entities;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public AuthenticationResult Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user){
            throw new Exception("Invalid user");
        }
        if(user.Password != password){
            throw new Exception("Invalid Password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {

       if(_userRepository.GetUserByEmail(Email) != null){
         throw new Exception("User already present");
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