using BuberDiner.Domain.Entities;
using BuberDinner.Application.common.Interfaces.Persistence;

namespace BuberDiner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{

    private static List<User> users = new();
    public void Add(User user)
    {
        users.Add(user);

    }

    public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(x=>x.Email == email);
    }
}