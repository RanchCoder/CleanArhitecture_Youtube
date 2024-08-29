using BuberDiner.Domain.Entities;

namespace BuberDinner.Application.common.Interfaces.Persistence;

public interface IUserRepository{
    User? GetUserByEmail(string email);
    void Add(User user);
}
