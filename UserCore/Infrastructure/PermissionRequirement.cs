using Microsoft.AspNetCore.Authorization;
using UserCore.Entities;
using UserCore.Enums;

namespace UserCore.Infrastructure;

public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}