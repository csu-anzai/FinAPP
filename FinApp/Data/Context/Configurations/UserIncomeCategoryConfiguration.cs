using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configurations
{
    public class UserIncomeCategoryConfiguration : IEntityTypeConfiguration<UserIncomeCategory>
    {
        public void Configure(EntityTypeBuilder<UserIncomeCategory> builder)
        {
            builder.HasKey(uc => new { uc.IncomeCategoryId, uc.UserId });

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserIncomeCategories)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.IncomeCategory)
                .WithMany(u => u.UserIncomeCategories)
                .HasForeignKey(uc => uc.IncomeCategoryId);
        }
    }
}
