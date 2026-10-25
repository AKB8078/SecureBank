namespace WebApplication1.Entities
{
    public class IdempotencyRecord
    {
        public int IdempotencyRecordId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string IdempotencyKey { get; set; } = string.Empty;
        public string RequestHash { get; set; } = string.Empty;
        public string? ResponseJson { get; set; }
        public string RecordStatus { get; set; } = "Processing";
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; }

        public User User { get; set; } = null!;
    }
}
