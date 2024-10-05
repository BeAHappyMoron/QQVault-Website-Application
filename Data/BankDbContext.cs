using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QQVault.Models;

public class BankDbContext : IdentityDbContext<User>
{
    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Advice> Advices { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<BankAccount>().ToTable("BankAccount");
        modelBuilder.Entity<Transaction>().ToTable("Transaction");
        modelBuilder.Entity<Advice>().ToTable("Advices");

    }
}
