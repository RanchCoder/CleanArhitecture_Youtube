namespace BuberDinner.Application.common.Interfaces.Authentication;

public interface IJwtTokenGenerator{
    public string GenerateToken(Guid userId, string firstName, string lastName);
}