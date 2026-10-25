namespace WebApplication1.Entities
{
    public class MfaChallenge
    {
        public int MfaChallengeId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId
        public int? MfaMethodId { get; set; } // Optional FK -> MfaMethod.MfaMethodId

        public string ChallengeType { get; set; } = string.Empty;
        public string? CodeHash { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? ConsumedAtUtc { get; set; }
        public int FailedAttempts { get; set; }

        public User User { get; set; } = null!;
        public MfaMethod? MfaMethod { get; set; }
    }
}
