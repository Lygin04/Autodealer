using UserCore.DTOs;
using UserCore.Entities;
using UserCore.Infrastructure;
using UserCore.Interfaces.Auth;
using UserCore.Interfaces.Repositories;
using UserCore.Repositories;

namespace UserCore.Services;

public class UserService(
    IPasswordHasher passwordHasher, 
    IUserRepository userRepository, 
    IJwtProvider jwtProvider) : IUserService
{
    public async Task Register(RegisterDto registerDto)
    {
        var hashedPassword = passwordHasher.Generate(registerDto.Password);
        
        User user = new User
        {
            Email = registerDto.Email,
            Password = hashedPassword,
            Name = registerDto.Name,
            Phone = registerDto.Phone
        };

        await userRepository.Add(user);
    }

    public async Task<string> Login(LoginDto login)
    {
        var user = await userRepository.GetByEmail(login.Email);
        var result = passwordHasher.Verify(login.Password, user.Password);
        if (!result)
            throw new Exception();
        var token = jwtProvider.GenerateToken(user);
        return token;
    }
}