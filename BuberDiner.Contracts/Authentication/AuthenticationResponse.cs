using System.Security.Cryptography.X509Certificates;

namespace BuberDinner.Contracts.Authentication;

public record AuthenticationResponse(
   Guid Id,
   string FirstName,
   string LastName,
   string Email,
   string Token 
);