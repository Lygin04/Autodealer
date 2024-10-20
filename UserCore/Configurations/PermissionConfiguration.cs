using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserCore.Entities;
using UserCore.Enums;

namespace UserCore.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(p => p.Id);

        var permission = Enum.GetValues<Permission>().Select(p => new PermissionEntity
        {
            Id = (int)p,
            Name = p.ToString()
        });

        builder.HasData(permission);
    }
}