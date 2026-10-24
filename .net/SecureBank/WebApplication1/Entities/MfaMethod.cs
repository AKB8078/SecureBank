namespace WebApplication1.Entities
{
    public class MfaMethod
    {
        public int MfaMethodId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string MethodType { get; set; } = string.Empty; // TOTP, Email, etc.
        public string? EncryptedSecret { get; set; }
        public string? DestinationMasked { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastVerifiedAtUtc { get; set; }

        public User User { get; set; } = null!;
        public ICollection<MfaChallenge> MfaChallenges { get; set; } = new List<MfaChallenge>();
    }
}
