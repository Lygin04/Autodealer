using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserCore.Entities;
using UserCore.Interfaces.Repositories;

namespace UserCore.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task Add(UserEntity userEntity)
    {
        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ??
                   throw new Exception();
        return user;
    }
}