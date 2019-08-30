using DAL.Context.Configurations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class FinAppContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserExpenseCategory> UserExpenseCategories { get; set; }
        public DbSet<UserIncomeCategory> UserIncomeCategories { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<PasswordConfirmationCode> PasswordConfirmationCodes { get; set; }

        public FinAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserExpenseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserIncomeCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
}
