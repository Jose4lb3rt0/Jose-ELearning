using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class CuestionarioToProgresoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuestionarioID",
                table: "Progresos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progresos_CuestionarioID",
                table: "Progresos",
                column: "CuestionarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresos_Cuestionarios_CuestionarioID",
                table: "Progresos",
                column: "CuestionarioID",
                principalTable: "Cuestionarios",
                principalColumn: "CuestionarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresos_Cuestionarios_CuestionarioID",
                table: "Progresos");

            migrationBuilder.DropIndex(
                name: "IX_Progresos_CuestionarioID",
                table: "Progresos");

            migrationBuilder.DropColumn(
                name: "CuestionarioID",
                table: "Progresos");
        }
    }
}
