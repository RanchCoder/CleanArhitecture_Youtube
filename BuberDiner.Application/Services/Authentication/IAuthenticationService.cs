namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService{
    AuthenticationResult Login(string email, string password);
    AuthenticationResult Register(string FirstName, string LastName, string Email, string Password);
}