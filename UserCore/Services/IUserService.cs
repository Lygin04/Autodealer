using UserCore.DTOs;
using UserCore.Entities;

namespace UserCore.Services;

public interface IUserService
{
    Task<User> Register(RegisterDto registerDto);
    Task Login(LoginDto login);
    Task Delete(string id);
}