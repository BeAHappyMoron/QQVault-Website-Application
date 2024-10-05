namespace QQVault.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public User User { get; set; }
        public List<Transaction> AllTransactions { get; set; } // Admin views all transactions
        public decimal TotalMoneyTransferred { get; set; }
        public decimal TotalMoneyWithdrawn { get; set; }
        public int TotalTransactions { get; set; } // Total number of transactions
    }
}
