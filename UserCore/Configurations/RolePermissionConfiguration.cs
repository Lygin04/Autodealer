using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserCore.Entities;
using UserCore.Repositories;

namespace UserCore.Configurations;

public class RolePermissionConfiguration(AuthorizationOptions authorization) : IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });
        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return authorization.Role
    }
}