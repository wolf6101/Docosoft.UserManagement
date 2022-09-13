using Docosoft.UserManagement.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasData( new List<UserGroup> {
             new UserGroup (
                new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c"),
                "System Users",
                "System Users group"
            ),
            new UserGroup (
                new Guid("6b93145f-3ad7-4af4-bc2e-dea50293948a"),
                "Test Users",
                "Users created for testing purposes"
            ),
            new UserGroup (
                new Guid("63350483-da5f-4e77-bc5e-ccd66cdda50e"),
                "Developers",
                "Developers users"
            ),
            new UserGroup (
                new Guid("fe983c0b-6e05-40c6-97b8-b2d8867f442d"),
                "Quality Assurance",
                "Quality Assurance users"
            ),
            new UserGroup (
                new Guid("0cbbf9ac-3134-44ab-858f-32f4610df050"),
                "Interns",
                "Interns Users"
            )
        });
    }
}