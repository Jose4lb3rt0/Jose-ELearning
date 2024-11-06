using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCursoPrecioPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Cursos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_InstructorId",
                table: "Cursos",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_AspNetUsers_InstructorId",
                table: "Cursos",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_AspNetUsers_InstructorId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_InstructorId",
                table: "Cursos");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
