using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(u => u.Expense)
                .WithOne(t => t.Transaction)
                .HasForeignKey<Expense>(t => t.TransactionId);

            builder.HasOne(u => u.Income)
              .WithOne(t => t.Transaction)
              .HasForeignKey<Income>(t => t.TransactionId);

        }
    }
}
