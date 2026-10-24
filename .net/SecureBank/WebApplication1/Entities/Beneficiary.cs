namespace WebApplication1.Entities
{
    public class Beneficiary
    {
        public int BeneficiaryId { get; set; } // PK
        public int UserId { get; set; } // FK -> User.UserId
        public int? BankAccountId { get; set; } // Optional FK -> BankAccount.BankAccountId

        public string BeneficiaryName { get; set; } = string.Empty;
        public string BeneficiaryAccountNumber { get; set; } = string.Empty;
        public string? BeneficiaryBankName { get; set; }
        public string? IfscCode { get; set; }
        public string? Nickname { get; set; }
        public string BeneficiaryStatus { get; set; } = "Pending";
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAtUtc { get; set; }

        public User User { get; set; } = null!;
        public BankAccount? BankAccount { get; set; }
    }
}
