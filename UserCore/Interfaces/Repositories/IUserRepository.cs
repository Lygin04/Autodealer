using UserCore.Entities;

namespace UserCore.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
}