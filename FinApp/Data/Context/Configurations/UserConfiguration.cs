using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<Token>(t => t.UserId);

            builder.HasOne(u => u.PasswordConfirmationCode)
                .WithOne(p => p.User)
                .HasForeignKey<PasswordConfirmationCode>(p => p.UserId);

        }
    }
}
