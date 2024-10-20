using Microsoft.EntityFrameworkCore;
using UserCore.Entities;

namespace UserCore.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
}