namespace WebApplication1.Entities
{
    public class TransactionPinCredential
    {
        public int TransactionPinCredentialId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string PinHash { get; set; } = string.Empty;
        public int FailedAttempts { get; set; }
        public DateTime? LockedUntilUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }

        public User User { get; set; } = null!;
    }
}
