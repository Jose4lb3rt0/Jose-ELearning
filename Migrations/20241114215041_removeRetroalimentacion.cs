using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class removeRetroalimentacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retroalimentacion",
                table: "Calificaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Retroalimentacion",
                table: "Calificaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
