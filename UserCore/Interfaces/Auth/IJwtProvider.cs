using UserCore.Entities;

namespace UserCore.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity userEntity);
}