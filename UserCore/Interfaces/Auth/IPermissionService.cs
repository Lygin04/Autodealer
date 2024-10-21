using UserCore.Enums;

namespace UserCore.Interfaces.Auth;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}