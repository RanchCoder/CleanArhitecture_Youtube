using BuberDiner.Domain.Entities;
using BuberDiner.Domain.Errors;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using MediatR.Wrappers;

namespace BuberDiner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator){
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if(_userRepository.GetUserByEmail(request.Email) != null){
         return Errors.User.DuplicateEmail;
       }
       var user = new User{
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        Password = request.Password
       };

       _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}
