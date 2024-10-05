namespace QQVault.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Calculated total balance from all bank accounts.
        public decimal TotalBalance { get; set; }

        // User status based on their balance.
        public string UserStatus { get; set; }

        // A list of the user's bank accounts.
        public List<BankAccountViewModel> BankAccounts { get; set; }
        public List<AdviceViewModel> Advices { get; set; }
    }

    // ViewModel for bank account details.
    public class BankAccountViewModel
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

}
