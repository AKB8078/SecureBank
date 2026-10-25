namespace WebApplication1.Entities
{
    public class TrustedDevice
    {
        public int TrustedDeviceId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string DeviceTokenHash { get; set; } = string.Empty;
        public string? DeviceName { get; set; }
        public DateTime RegisteredAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastUsedAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }

        public User User { get; set; } = null!;
    }
}
