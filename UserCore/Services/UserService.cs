using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserCore.DTOs;
using UserCore.Entities;

namespace UserCore.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<User> Register(RegisterDto registerDto)
    {
        User user = new User
        {
            Email = registerDto.Email,
            Password = registerDto.Password,
            Name = registerDto.Name,
            Phone = registerDto.Phone
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task Login(LoginDto login)
    {
        User? user = await context.Users.Where(x => x.Email == login.Email && x.Password == login.Password)
            .FirstOrDefaultAsync();
        if (user == null)
            throw new InvalidOperationException();
    }

    public async Task Delete(string id)
    {
        User? user = await context.Users.Where(x => x.Id == Convert.ToUInt32(id)).FirstOrDefaultAsync();
        if (user == null)
            throw new InvalidOperationException();
        context.Users.Remove(user);
    }
}