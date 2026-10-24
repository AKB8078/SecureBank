using System.Security.Cryptography.Xml;

namespace WebApplication1.Entities
{
    public class BankAccount
    {
        public int BankAccountId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId

        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Savings";
        public string CurrencyCode { get; set; } = "INR";
        public decimal AvailableBalance { get; set; }
        public string AccountStatus { get; set; } = "Active";

        public DateTime OpenedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAtUtc { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
        public ICollection<Transfer> OutgoingTransfers { get; set; } = new List<Transfer>();
        public ICollection<Transfer> IncomingTransfers { get; set; } = new List<Transfer>();
        public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
    }
}
