using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddEstudiaCoinsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstudiaCoins",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstudiaCoins",
                table: "AspNetUsers");
        }
    }
}
