using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docosoft.UserManagement.Infrastructure.Migrations
{
    public partial class DefaultDataSeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0cbbf9ac-3134-44ab-858f-32f4610df050"), "Interns Users", "Interns" },
                    { new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c"), "System Users group", "System Users" },
                    { new Guid("63350483-da5f-4e77-bc5e-ccd66cdda50e"), "Developers users", "Developers" },
                    { new Guid("6b93145f-3ad7-4af4-bc2e-dea50293948a"), "Users created for testing purposes", "Test Users" },
                    { new Guid("fe983c0b-6e05-40c6-97b8-b2d8867f442d"), "Quality Assurance users", "Quality Assurance" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0813e00f-d49b-4675-b74a-0ab63bcf7404"), "User with access to API services", "API User" },
                    { new Guid("62bd5e7a-6366-4299-bfff-946593695c53"), "User with elevated access", "Administrator" },
                    { new Guid("bd4a08b6-8243-4754-9667-1bca049d0f4b"), "User with highest access", "SuperAdmin" },
                    { new Guid("c192a50e-be1a-41dc-b9a3-f9b986a20929"), "Customer user registered via form", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDateTime", "Email", "FirstName", "Gender", "LastName", "LastUpdatedDateTime", "UserRoleId" },
                values: new object[] { new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"), new DateTime(2022, 9, 13, 10, 42, 46, 450, DateTimeKind.Local).AddTicks(3374), "johnsmith@email.com", "Super", 0, "Admin", new DateTime(2022, 9, 13, 10, 42, 46, 450, DateTimeKind.Local).AddTicks(3405), new Guid("bd4a08b6-8243-4754-9667-1bca049d0f4b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("0cbbf9ac-3134-44ab-858f-32f4610df050"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("63350483-da5f-4e77-bc5e-ccd66cdda50e"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("6b93145f-3ad7-4af4-bc2e-dea50293948a"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("fe983c0b-6e05-40c6-97b8-b2d8867f442d"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("0813e00f-d49b-4675-b74a-0ab63bcf7404"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("62bd5e7a-6366-4299-bfff-946593695c53"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("c192a50e-be1a-41dc-b9a3-f9b986a20929"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("bd4a08b6-8243-4754-9667-1bca049d0f4b"));
        }
    }
}
