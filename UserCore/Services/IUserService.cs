using UserCore.DTOs;

namespace UserCore.Services;

public interface IUserService
{
    Task Register(RegisterDto registerDto);
    Task<string> Login(LoginDto login);
}