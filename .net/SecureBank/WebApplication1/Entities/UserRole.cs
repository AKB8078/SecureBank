using System.Data;

namespace WebApplication1.Entities
{
    public class UserRole
    {
        public int UserRoleId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId
        public int RoleId { get; set; } // FK -> Role.RoleId
        public DateTime AssignedAtUtc { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
