using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoleId",
                table: "RolePermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ApplicationRoleId",
                table: "RolePermissions",
                column: "ApplicationRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_AspNetRoles_ApplicationRoleId",
                table: "RolePermissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_AspNetRoles_ApplicationRoleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_ApplicationRoleId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "RolePermissions");
        }
    }
}
