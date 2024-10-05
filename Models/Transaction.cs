using System;
using System.ComponentModel.DataAnnotations;

namespace QQVault.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Enter the amount.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Select a transaction type.")]
        public string TransactionType { get; set; } // e.g., "Transfer"

        public DateTime CreatedAt { get; set; }

        // Foreign key to Source and Destination accounts
        public int SourceAccountId { get; set; }
        public BankAccount SourceAccount { get; set; }

        public int DestinationAccountId { get; set; }
        public BankAccount DestinationAccount { get; set; }
    }
}