using UserCore.Entities;

namespace UserCore.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(UserEntity userEntity);
    Task<UserEntity> GetByEmail(string email);
}