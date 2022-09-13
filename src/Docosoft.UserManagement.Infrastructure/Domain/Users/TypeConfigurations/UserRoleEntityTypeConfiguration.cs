using Docosoft.UserManagement.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Docosoft.UserManagement.Infrastructure.Domain.Users.TypeConfigurations
{
    class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(new List<UserRole> {
                new UserRole (
                    new Guid("62bd5e7a-6366-4299-bfff-946593695c53"),
                    "Administrator",
                    "User with elevated access"
                ),
                new UserRole (
                    new Guid("bd4a08b6-8243-4754-9667-1bca049d0f4b"),
                    "SuperAdmin",
                    "User with highest access"
                ),
                new UserRole (
                    new Guid("0813e00f-d49b-4675-b74a-0ab63bcf7404"),
                    "API User",
                    "User with access to API services"
                ),
                new UserRole (
                    new Guid("c192a50e-be1a-41dc-b9a3-f9b986a20929"),
                    "Customer",
                    "Customer user registered via form"
                ),
            });
        }
    }
}