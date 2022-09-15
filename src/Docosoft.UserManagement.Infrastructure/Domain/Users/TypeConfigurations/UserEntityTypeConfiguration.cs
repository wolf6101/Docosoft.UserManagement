using Docosoft.UserManagement.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Docosoft.UserManagement.Infrastructure.Domain.Users.TypeConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne<UserRole>(u => u.Role)
                   .WithMany(r => r.Users)
                   .IsRequired(true)
                   .HasForeignKey(u => u.UserRoleId);

            builder.HasMany(p => p.Groups)
                   .WithMany(p => p.Users)
                   .UsingEntity(j => j.ToTable("UserUserGroup")
                   .HasData(new
                   {
                       UsersId = new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"),
                       GroupsId = new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c")
                   }));

            builder.HasData(new List<User> {
                new User (
                    new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"),
                    "Super",
                    "Admin",
                    GenderEnum.Unknown,
                    "johnsmith@email.com",
                    new Guid("bd4a08b6-8243-4754-9667-1bca049d0f4b")
                ),
            });
        }
    }
}