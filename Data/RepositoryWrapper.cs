using QQVault.Data;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly BankDbContext _bankDbContext;
    private IUserRepository _user;
    private ITransactionRepository _transaction;
    private IBankAccountRepository _bankaccount;
    private IFeedbackRepository _feedback;
// Add this line
    private IAdviceRepository _advice;


    public RepositoryWrapper(BankDbContext bankDbContext)
    {
        _bankDbContext = bankDbContext;
    }

    public IUserRepository User
    {
        get
        {
            if (_user == null)
            {
                _user = new UserRepository(_bankDbContext);
            }
            return _user;
        }
    }

    public ITransactionRepository Transaction
    {
        get
        {
            if (_transaction == null)
            {
                _transaction = new TransactionRepository(_bankDbContext, BankAccount);
            }
            return _transaction;
        }
    }

    public IBankAccountRepository BankAccount
    {
        get
        {
            if (_bankaccount == null)
            {
                _bankaccount = new BankAccountRepository(_bankDbContext);
            }
            return _bankaccount;
        }
    }

    public IFeedbackRepository Feedback
    {
        get
        {
            if (_feedback == null)
            {
                _feedback = new FeedbackRepository(_bankDbContext);
            }
            return _feedback;
        }
    }


    public IAdviceRepository Advice
    {
        get
        {
            if (_advice == null)
            {
                _advice = new AdviceRepository(_bankDbContext);
            }
            return _advice;
        }
    }     

   


    public void Save()
    {
        _bankDbContext.SaveChanges();
    }
}
