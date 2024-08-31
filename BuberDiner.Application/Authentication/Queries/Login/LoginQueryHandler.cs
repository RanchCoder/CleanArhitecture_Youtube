using BuberDiner.Domain.Entities;
using BuberDiner.Domain.Errors;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDiner.Application.Authentication.Quries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator){
          _userRepository = userRepository;
          _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
         if(_userRepository.GetUserByEmail(request.Email) is not User user){
            return Errors.InvalidCredentials.InvalidCredential;
        }
        if(user.Password != request.Password){
            return Errors.InvalidCredentials.InvalidCredential;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}