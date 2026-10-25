namespace WebApplication1.Entities
{
    public class AuditLog
    {
        public long AuditLogId { get; set; } // PK; long for log volume
        public int? UserId { get; set; } // Optional FK -> User.UserId

        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string? EntityIdentifier { get; set; }
        public string? IpAddress { get; set; }
        public string? CorrelationId { get; set; }
        public string? DetailsJson { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
    }
}
