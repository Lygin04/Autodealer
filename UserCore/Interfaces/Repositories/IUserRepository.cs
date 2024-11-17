using UserCore.DTOs;
using UserCore.Entities;
using UserCore.Enums;

namespace UserCore.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(RegisterDto userEntity);
    Task<UserEntity> GetByEmail(string email);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}