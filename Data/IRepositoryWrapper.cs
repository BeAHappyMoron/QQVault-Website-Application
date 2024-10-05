namespace QQVault.Data
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ITransactionRepository Transaction { get; }
        IBankAccountRepository BankAccount { get; }
        IFeedbackRepository Feedback { get; }
        IAdviceRepository Advice { get; }



        void Save();
    }
}
