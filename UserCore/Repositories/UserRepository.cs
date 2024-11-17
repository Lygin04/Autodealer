using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserCore.DTOs;
using UserCore.Entities;
using UserCore.Enums;
using UserCore.Interfaces.Repositories;

namespace UserCore.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task Add(RegisterDto user)
    {
        var roleEntity = await context.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)Role.User) ?? throw new InvalidOperationException();

        var newUser = new UserEntity
        {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            Phone = user.Phone,
            Roles = [roleEntity]
        };
            
        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ??
                   throw new Exception();
        return user;
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles.SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }
}