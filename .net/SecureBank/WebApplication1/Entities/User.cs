namespace WebApplication1.Entities
{
    public class User
    {
        public int UserId { get; set; } // PK

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailNormalized { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string UserStatus { get; set; } = "Active";
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockedUntilUtc { get; set; }
        public DateTime? LastLoginUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
        public ICollection<MfaMethod> MfaMethods { get; set; } = new List<MfaMethod>();
        public ICollection<MfaChallenge> MfaChallenges { get; set; } = new List<MfaChallenge>();
        public ICollection<RecoveryCode> RecoveryCodes { get; set; } = new List<RecoveryCode>();
        public ICollection<TrustedDevice> TrustedDevices { get; set; } = new List<TrustedDevice>();
        public ICollection<IdempotencyRecord> IdempotencyRecords { get; set; } = new List<IdempotencyRecord>();
    }
}
