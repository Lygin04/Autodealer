using Microsoft.AspNetCore.Authorization;
using UserCore.Enums;

namespace UserCore.Infrastructure;

public class RequirePermissionAttribute : AuthorizeAttribute
{
    public RequirePermissionAttribute(params Permission[] permissions)
    {
        var policyName = string.Join(",", permissions.Select(p => p.ToString()));
        Policy = $"Permission:{policyName}";
    }
}