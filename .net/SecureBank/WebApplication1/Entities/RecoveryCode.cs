namespace WebApplication1.Entities
{
    public class RecoveryCode
    {
        public int RecoveryCodeId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string CodeHash { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UsedAtUtc { get; set; }

        public User User { get; set; } = null!;
    }
}
