using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class SecureBankDbContext:DbContext
    {
        public SecureBankDbContext(DbContextOptions<SecureBankDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();
        public DbSet<Transfer> Transfers => Set<Transfer>();
        public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<MfaMethod> MfaMethods => Set<MfaMethod>();
        public DbSet<MfaChallenge> MfaChallenges => Set<MfaChallenge>();
        public DbSet<RecoveryCode> RecoveryCodes => Set<RecoveryCode>();
        public DbSet<TransactionPinCredential> TransactionPinCredentials
            => Set<TransactionPinCredential>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<TrustedDevice> TrustedDevices => Set<TrustedDevice>();
        public DbSet<IdempotencyRecord> IdempotencyRecords
            => Set<IdempotencyRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.UserId).HasName("PK_Users");

                entity.Property(x => x.UserId).ValueGeneratedOnAdd();
                entity.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(254).IsRequired();
                entity.Property(x => x.EmailNormalized).HasMaxLength(254).IsRequired();
                entity.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
                entity.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
                entity.Property(x => x.UserStatus).HasMaxLength(20).IsRequired();

                entity.HasIndex(x => x.EmailNormalized)
                    .IsUnique()
                    .HasDatabaseName("UX_Users_EmailNormalized");

                entity.HasIndex(x => x.PhoneNumber)
                    .HasDatabaseName("IX_Users_PhoneNumber");
            });

            // ROLES
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(x => x.RoleId).HasName("PK_Roles");

                entity.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(250).IsRequired();

                entity.HasIndex(x => x.RoleName)
                    .IsUnique()
                    .HasDatabaseName("UX_Roles_RoleName");
            });

            // USER ROLES: many-to-many join table
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasKey(x => x.UserRoleId).HasName("PK_UserRoles");

                entity.HasIndex(x => new { x.UserId, x.RoleId })
                    .IsUnique()
                    .HasDatabaseName("UX_UserRoles_UserId_RoleId");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_UserRoles_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .HasConstraintName("FK_UserRoles_Roles_RoleId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // BANK ACCOUNTS
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.ToTable("BankAccounts");
                entity.HasKey(x => x.BankAccountId).HasName("PK_BankAccounts");

                entity.Property(x => x.AccountNumber).HasMaxLength(34).IsRequired();
                entity.Property(x => x.AccountType).HasMaxLength(30).IsRequired();
                entity.Property(x => x.CurrencyCode).HasMaxLength(3).IsRequired();
                entity.Property(x => x.AccountStatus).HasMaxLength(20).IsRequired();
                entity.Property(x => x.AvailableBalance).HasPrecision(19, 4);

                entity.HasIndex(x => x.AccountNumber)
                    .IsUnique()
                    .HasDatabaseName("UX_BankAccounts_AccountNumber");

                entity.HasIndex(x => x.UserId)
                    .HasDatabaseName("IX_BankAccounts_UserId");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.BankAccounts)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_BankAccounts_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // BENEFICIARIES
            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.ToTable("Beneficiaries");
                entity.HasKey(x => x.BeneficiaryId).HasName("PK_Beneficiaries");

                entity.Property(x => x.BeneficiaryName).HasMaxLength(150).IsRequired();
                entity.Property(x => x.BeneficiaryAccountNumber).HasMaxLength(34).IsRequired();
                entity.Property(x => x.BeneficiaryBankName).HasMaxLength(150);
                entity.Property(x => x.IfscCode).HasMaxLength(11);
                entity.Property(x => x.Nickname).HasMaxLength(100);
                entity.Property(x => x.BeneficiaryStatus).HasMaxLength(20).IsRequired();

                entity.HasIndex(x => x.UserId)
                    .HasDatabaseName("IX_Beneficiaries_UserId");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.Beneficiaries)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_Beneficiaries_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.BankAccount)
                    .WithMany(x => x.Beneficiaries)
                    .HasForeignKey(x => x.BankAccountId)
                    .HasConstraintName("FK_Beneficiaries_BankAccounts_BankAccountId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TRANSFERS
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfers");
                entity.HasKey(x => x.TransferId).HasName("PK_Transfers");

                entity.Property(x => x.Amount).HasPrecision(19, 4);
                entity.Property(x => x.CurrencyCode).HasMaxLength(3).IsRequired();
                entity.Property(x => x.TransferType).HasMaxLength(30).IsRequired();
                entity.Property(x => x.TransferStatus).HasMaxLength(30).IsRequired();
                entity.Property(x => x.ReferenceNumber).HasMaxLength(100);
                entity.Property(x => x.Description).HasMaxLength(500);

                entity.ToTable("Transfers", table =>
                {
                    table.HasCheckConstraint(
                        "CK_Transfers_Amount_Positive",
                        "\"Amount\" > 0");

                    table.HasCheckConstraint(
                        "CK_Transfers_DifferentAccounts",
                        "\"FromBankAccountId\" <> \"ToBankAccountId\"");
                });

                entity.HasIndex(x => x.ReferenceNumber)
                    .IsUnique()
                    .HasDatabaseName("UX_Transfers_ReferenceNumber")
                    .HasFilter("\"ReferenceNumber\" IS NOT NULL");

                entity.HasIndex(x => x.FromBankAccountId)
                    .HasDatabaseName("IX_Transfers_FromBankAccountId");

                entity.HasIndex(x => x.ToBankAccountId)
                    .HasDatabaseName("IX_Transfers_ToBankAccountId");

                entity.HasOne(x => x.FromBankAccount)
                    .WithMany(x => x.OutgoingTransfers)
                    .HasForeignKey(x => x.FromBankAccountId)
                    .HasConstraintName("FK_Transfers_FromBankAccount")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ToBankAccount)
                    .WithMany(x => x.IncomingTransfers)
                    .HasForeignKey(x => x.ToBankAccountId)
                    .HasConstraintName("FK_Transfers_ToBankAccount")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Beneficiary)
                    .WithMany()
                    .HasForeignKey(x => x.BeneficiaryId)
                    .HasConstraintName("FK_Transfers_Beneficiaries_BeneficiaryId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // LEDGER ENTRIES
            modelBuilder.Entity<LedgerEntry>(entity =>
            {
                entity.ToTable("LedgerEntries");
                entity.HasKey(x => x.LedgerEntryId).HasName("PK_LedgerEntries");

                entity.Property(x => x.EntryType).HasMaxLength(10).IsRequired();
                entity.Property(x => x.Amount).HasPrecision(19, 4);
                entity.Property(x => x.CurrencyCode).HasMaxLength(3).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);

                entity.ToTable("LedgerEntries", table =>
                {
                    table.HasCheckConstraint(
                        "CK_LedgerEntries_Amount_Positive",
                        "\"Amount\" > 0");

                    table.HasCheckConstraint(
                        "CK_LedgerEntries_EntryType",
                        "\"EntryType\" IN ('Debit', 'Credit')");
                });

                entity.HasIndex(x => new { x.TransferId, x.BankAccountId })
                    .HasDatabaseName("IX_LedgerEntries_TransferId_BankAccountId");

                entity.HasOne(x => x.Transfer)
                    .WithMany(x => x.LedgerEntries)
                    .HasForeignKey(x => x.TransferId)
                    .HasConstraintName("FK_LedgerEntries_Transfers_TransferId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.BankAccount)
                    .WithMany(x => x.LedgerEntries)
                    .HasForeignKey(x => x.BankAccountId)
                    .HasConstraintName("FK_LedgerEntries_BankAccounts_BankAccountId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // USER SESSIONS
            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.ToTable("UserSessions");
                entity.HasKey(x => x.UserSessionId).HasName("PK_UserSessions");

                entity.Property(x => x.DeviceName).HasMaxLength(200);
                entity.Property(x => x.IpAddress).HasMaxLength(45);
                entity.Property(x => x.UserAgent).HasMaxLength(1000);

                entity.HasIndex(x => x.UserId)
                    .HasDatabaseName("IX_UserSessions_UserId");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.UserSessions)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_UserSessions_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // REFRESH TOKENS
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(x => x.RefreshTokenId).HasName("PK_RefreshTokens");

                entity.Property(x => x.TokenHash).HasMaxLength(500).IsRequired();

                entity.HasIndex(x => x.TokenHash)
                    .IsUnique()
                    .HasDatabaseName("UX_RefreshTokens_TokenHash");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.RefreshTokens)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_RefreshTokens_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.UserSession)
                    .WithMany(x => x.RefreshTokens)
                    .HasForeignKey(x => x.UserSessionId)
                    .HasConstraintName("FK_RefreshTokens_UserSessions_UserSessionId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ReplacedByRefreshToken)
                    .WithMany()
                    .HasForeignKey(x => x.ReplacedByRefreshTokenId)
                    .HasConstraintName("FK_RefreshTokens_ReplacedBy")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // MFA METHODS
            modelBuilder.Entity<MfaMethod>(entity =>
            {
                entity.ToTable("MfaMethods");
                entity.HasKey(x => x.MfaMethodId).HasName("PK_MfaMethods");

                entity.Property(x => x.MethodType).HasMaxLength(30).IsRequired();
                entity.Property(x => x.EncryptedSecret).HasMaxLength(2000);
                entity.Property(x => x.DestinationMasked).HasMaxLength(200);

                entity.HasIndex(x => x.UserId)
                    .HasDatabaseName("IX_MfaMethods_UserId");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.MfaMethods)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_MfaMethods_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // MFA CHALLENGES
            modelBuilder.Entity<MfaChallenge>(entity =>
            {
                entity.ToTable("MfaChallenges");
                entity.HasKey(x => x.MfaChallengeId).HasName("PK_MfaChallenges");

                entity.Property(x => x.ChallengeType).HasMaxLength(30).IsRequired();
                entity.Property(x => x.CodeHash).HasMaxLength(500);

                entity.HasOne(x => x.User)
                    .WithMany(x => x.MfaChallenges)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_MfaChallenges_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.MfaMethod)
                    .WithMany(x => x.MfaChallenges)
                    .HasForeignKey(x => x.MfaMethodId)
                    .HasConstraintName("FK_MfaChallenges_MfaMethods_MfaMethodId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // RECOVERY CODES
            modelBuilder.Entity<RecoveryCode>(entity =>
            {
                entity.ToTable("RecoveryCodes");
                entity.HasKey(x => x.RecoveryCodeId).HasName("PK_RecoveryCodes");

                entity.Property(x => x.CodeHash).HasMaxLength(500).IsRequired();

                entity.HasIndex(x => new { x.UserId, x.CodeHash })
                    .IsUnique()
                    .HasDatabaseName("UX_RecoveryCodes_UserId_CodeHash");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.RecoveryCodes)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_RecoveryCodes_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TRANSACTION PIN CREDENTIALS
            modelBuilder.Entity<TransactionPinCredential>(entity =>
            {
                entity.ToTable("TransactionPinCredentials");
                entity.HasKey(x => x.TransactionPinCredentialId)
                    .HasName("PK_TransactionPinCredentials");

                entity.Property(x => x.PinHash).HasMaxLength(500).IsRequired();

                entity.HasIndex(x => x.UserId)
                    .IsUnique()
                    .HasDatabaseName("UX_TransactionPinCredentials_UserId");

                entity.HasOne(x => x.User)
                    .WithOne()
                    .HasForeignKey<TransactionPinCredential>(x => x.UserId)
                    .HasConstraintName("FK_TransactionPinCredentials_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // AUDIT LOGS
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.HasKey(x => x.AuditLogId).HasName("PK_AuditLogs");

                entity.Property(x => x.Action).HasMaxLength(100).IsRequired();
                entity.Property(x => x.EntityName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.EntityIdentifier).HasMaxLength(100);
                entity.Property(x => x.IpAddress).HasMaxLength(45);
                entity.Property(x => x.CorrelationId).HasMaxLength(100);
                entity.Property(x => x.DetailsJson).HasColumnType("TEXT");

                entity.HasIndex(x => x.CreatedAtUtc)
                    .HasDatabaseName("IX_AuditLogs_CreatedAtUtc");

                entity.HasIndex(x => x.UserId)
                    .HasDatabaseName("IX_AuditLogs_UserId");

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_AuditLogs_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TRUSTED DEVICES
            modelBuilder.Entity<TrustedDevice>(entity =>
            {
                entity.ToTable("TrustedDevices");
                entity.HasKey(x => x.TrustedDeviceId).HasName("PK_TrustedDevices");

                entity.Property(x => x.DeviceTokenHash).HasMaxLength(500).IsRequired();
                entity.Property(x => x.DeviceName).HasMaxLength(200);

                entity.HasIndex(x => new { x.UserId, x.DeviceTokenHash })
                    .IsUnique()
                    .HasDatabaseName("UX_TrustedDevices_UserId_DeviceTokenHash");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.TrustedDevices)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_TrustedDevices_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // IDEMPOTENCY RECORDS
            modelBuilder.Entity<IdempotencyRecord>(entity =>
            {
                entity.ToTable("IdempotencyRecords");
                entity.HasKey(x => x.IdempotencyRecordId)
                    .HasName("PK_IdempotencyRecords");

                entity.Property(x => x.IdempotencyKey).HasMaxLength(200).IsRequired();
                entity.Property(x => x.RequestHash).HasMaxLength(500).IsRequired();
                entity.Property(x => x.ResponseJson).HasColumnType("TEXT");
                entity.Property(x => x.RecordStatus).HasMaxLength(30).IsRequired();

                entity.HasIndex(x => new { x.UserId, x.IdempotencyKey })
                    .IsUnique()
                    .HasDatabaseName("UX_IdempotencyRecords_UserId_IdempotencyKey");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.IdempotencyRecords)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_IdempotencyRecords_Users_UserId")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
