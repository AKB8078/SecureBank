namespace WebApplication1.Entities
{
    public class Transfer
    {
        public int TransferId { get; set; } // PK
        public int FromBankAccountId { get; set; } // FK -> BankAccount.BankAccountId
        public int ToBankAccountId { get; set; } // FK -> BankAccount.BankAccountId
        public int? BeneficiaryId { get; set; } // Optional FK -> Beneficiary.BeneficiaryId

        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } = "INR";
        public string TransferType { get; set; } = "Internal";
        public string TransferStatus { get; set; } = "Pending";
        public string? ReferenceNumber { get; set; }
        public string? Description { get; set; }

        public DateTime InitiatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAtUtc { get; set; }

        public BankAccount FromBankAccount { get; set; } = null!;
        public BankAccount ToBankAccount { get; set; } = null!;
        public Beneficiary? Beneficiary { get; set; }

        public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
    }
}
