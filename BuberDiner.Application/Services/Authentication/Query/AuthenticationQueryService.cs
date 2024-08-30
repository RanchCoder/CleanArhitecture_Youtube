using System.Security.Cryptography.X509Certificates;
using BuberDiner.Application.Common.Errors;
using BuberDiner.Domain.Entities;
using BuberDiner.Domain.Errors;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Query;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository){
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

}