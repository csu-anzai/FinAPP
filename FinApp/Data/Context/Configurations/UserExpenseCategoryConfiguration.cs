using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.Configurations
{
    public class UserExpenseCategoryConfiguration : IEntityTypeConfiguration<UserExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<UserExpenseCategory> builder)
        {
            builder.HasKey(uc => new { uc.ExpenseCategoryId, uc.UserId});

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserExpenseCategories)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.ExpenseCategory)
                .WithMany(u => u.UserExpenseCategories)
                .HasForeignKey(uc => uc.ExpenseCategoryId);
        }
    }
}
