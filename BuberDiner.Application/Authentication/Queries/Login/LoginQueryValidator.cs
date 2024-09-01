using FluentValidation;

namespace BuberDiner.Application.Authentication.Quries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>{
    public LoginQueryValidator(){
        RuleFor(x=>x.Email).NotEmpty();
        RuleFor(x=>x.Password).NotEmpty();
    }
}