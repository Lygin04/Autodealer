using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserCore.Entities;
using UserCore.Interfaces.Repositories;

namespace UserCore.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task Add(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ??
                   throw new Exception();
        return user;
    }
}