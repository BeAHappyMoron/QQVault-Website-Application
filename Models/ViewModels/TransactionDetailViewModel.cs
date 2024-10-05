namespace QQVault.Models.ViewModels
{
    public class TransactionDetailViewModel
    {
        public string SourceName { get; set; }
        public string SourceSurname { get; set; }
        public string SourceAccountNumber { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverAccountNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
