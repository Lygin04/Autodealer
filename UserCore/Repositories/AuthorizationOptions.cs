using UserCore.Entities;

namespace UserCore.Repositories;

public class AuthorizationOptions
{
    public RolePermissions[] RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { set; get; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}