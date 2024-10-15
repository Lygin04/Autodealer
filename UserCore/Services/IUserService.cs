using UserCore.DTOs;
using UserCore.Entities;

namespace UserCore.Services;

public interface IUserService
{
    Task Register(RegisterDto registerDto);
    Task<string> Login(LoginDto login);
}