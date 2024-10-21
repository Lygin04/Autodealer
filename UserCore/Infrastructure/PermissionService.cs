using UserCore.Enums;
using UserCore.Interfaces.Auth;
using UserCore.Interfaces.Repositories;

namespace UserCore.Infrastructure;

public class PermissionService(IUserRepository userRepository) : IPermissionService
{
    public async Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        return await userRepository.GetUserPermissions(userId);
    }
}