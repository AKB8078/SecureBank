namespace WebApplication1.Entities
{
    public class LedgerEntry
    {
        public int LedgerEntryId { get; set; } // PK
        public int TransferId { get; set; } // FK -> Transfer.TransferId
        public int BankAccountId { get; set; } // FK -> BankAccount.BankAccountId

        public string EntryType { get; set; } = string.Empty; // Debit or Credit
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } = "INR";
        public DateTime PostedAtUtc { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }

        public Transfer Transfer { get; set; } = null!;
        public BankAccount BankAccount { get; set; } = null!;
    }
}
