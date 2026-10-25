namespace WebApplication1.Entities
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId
        public int UserSessionId { get; set; } // FK -> UserSession.UserSessionId

        public string TokenHash { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public int? ReplacedByRefreshTokenId { get; set; } // Optional self-FK

        public User User { get; set; } = null!;
        public UserSession UserSession { get; set; } = null!;
        public RefreshToken? ReplacedByRefreshToken { get; set; }
    }
}
