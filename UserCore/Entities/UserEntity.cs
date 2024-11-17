using System.ComponentModel.DataAnnotations;

namespace UserCore.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public ICollection<RoleEntity> Roles { get; set; } = [];
}