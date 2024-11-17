using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserCore.Configurations;
using UserCore.Entities;
using UserCore.Repositories;

namespace UserCore.Data;

public class DataContext(DbContextOptions<DataContext> options,
    IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
    }
}