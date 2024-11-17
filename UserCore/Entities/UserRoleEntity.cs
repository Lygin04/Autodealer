namespace UserCore.Entities;

public class UserRoleEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
    
    public UserEntity User { get; set; }
    public RoleEntity Role { get; set; }
}