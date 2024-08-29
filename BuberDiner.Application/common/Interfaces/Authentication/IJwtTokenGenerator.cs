using BuberDiner.Domain.Entities;

namespace BuberDinner.Application.common.Interfaces.Authentication;

public interface IJwtTokenGenerator{
    public string GenerateToken(User user);
}