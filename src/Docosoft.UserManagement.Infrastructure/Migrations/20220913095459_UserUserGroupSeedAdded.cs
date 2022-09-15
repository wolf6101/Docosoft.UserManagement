using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docosoft.UserManagement.Infrastructure.Migrations
{
    public partial class UserUserGroupSeedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserUserGroup",
                columns: new[] { "GroupsId", "UsersId" },
                values: new object[] { new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c"), new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"),
                columns: new[] { "CreatedDateTime", "LastUpdatedDateTime" },
                values: new object[] { new DateTime(2022, 9, 13, 10, 54, 58, 981, DateTimeKind.Local).AddTicks(1526), new DateTime(2022, 9, 13, 10, 54, 58, 981, DateTimeKind.Local).AddTicks(1560) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserUserGroup",
                keyColumns: new[] { "GroupsId", "UsersId" },
                keyValues: new object[] { new Guid("41e1ae0a-8af9-405a-93ff-ab3f24d7538c"), new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a75901a6-ab5d-4027-8816-a289e0714e1f"),
                columns: new[] { "CreatedDateTime", "LastUpdatedDateTime" },
                values: new object[] { new DateTime(2022, 9, 13, 10, 42, 46, 450, DateTimeKind.Local).AddTicks(3374), new DateTime(2022, 9, 13, 10, 42, 46, 450, DateTimeKind.Local).AddTicks(3405) });
        }
    }
}