using Docosoft.UserManagement.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne<UserRole>(u => u.Role)
               .WithMany(r => r.Users)
               .IsRequired(true)
               .HasForeignKey(u => u.UserRoleId);
    }
}