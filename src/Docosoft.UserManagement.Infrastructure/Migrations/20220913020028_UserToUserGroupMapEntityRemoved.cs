using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docosoft.UserManagement.Infrastructure.Migrations
{
    public partial class UserToUserGroupMapEntityRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToUserGroupMap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToUserGroupMap",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserGroupMap", x => new { x.UserId, x.UserGroupId });
                    table.ForeignKey(
                        name: "FK_UserToUserGroupMap_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserGroupMap_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserGroupMap_UserGroupId",
                table: "UserToUserGroupMap",
                column: "UserGroupId");
        }
    }
}
