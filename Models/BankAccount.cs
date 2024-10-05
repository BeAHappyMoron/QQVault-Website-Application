using QQVault.Models;
using System.ComponentModel.DataAnnotations;

public class BankAccount
{
    [Key]
    public int AccountId { get; set; }

    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }

    // Change UserId type to string
    public string UserId { get; set; } // Updated to string
    public User User { get; set; }
}