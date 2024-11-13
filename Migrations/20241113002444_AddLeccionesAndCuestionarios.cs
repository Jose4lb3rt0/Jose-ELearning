using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddLeccionesAndCuestionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leccion_Modulos_ModuloID",
                table: "Leccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leccion",
                table: "Leccion");

            migrationBuilder.RenameTable(
                name: "Leccion",
                newName: "Lecciones");

            migrationBuilder.RenameColumn(
                name: "contenido",
                table: "Lecciones",
                newName: "Contenido");

            migrationBuilder.RenameColumn(
                name: "ModuloID",
                table: "Lecciones",
                newName: "modulo_id");

            migrationBuilder.RenameColumn(
                name: "LeccionID",
                table: "Lecciones",
                newName: "leccion_id");

            migrationBuilder.RenameIndex(
                name: "IX_Leccion_ModuloID",
                table: "Lecciones",
                newName: "IX_Lecciones_modulo_id");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "Lecciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoContenido",
                table: "Lecciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecciones",
                table: "Lecciones",
                column: "leccion_id");

            migrationBuilder.CreateTable(
                name: "Cuestionarios",
                columns: table => new
                {
                    cuestionario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    leccion_id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionarios", x => x.cuestionario_id);
                    table.ForeignKey(
                        name: "FK_Cuestionarios_Lecciones_leccion_id",
                        column: x => x.leccion_id,
                        principalTable: "Lecciones",
                        principalColumn: "leccion_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuestionarios_leccion_id",
                table: "Cuestionarios",
                column: "leccion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecciones_Modulos_modulo_id",
                table: "Lecciones",
                column: "modulo_id",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecciones_Modulos_modulo_id",
                table: "Lecciones");

            migrationBuilder.DropTable(
                name: "Cuestionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecciones",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "TipoContenido",
                table: "Lecciones");

            migrationBuilder.RenameTable(
                name: "Lecciones",
                newName: "Leccion");

            migrationBuilder.RenameColumn(
                name: "Contenido",
                table: "Leccion",
                newName: "contenido");

            migrationBuilder.RenameColumn(
                name: "modulo_id",
                table: "Leccion",
                newName: "ModuloID");

            migrationBuilder.RenameColumn(
                name: "leccion_id",
                table: "Leccion",
                newName: "LeccionID");

            migrationBuilder.RenameIndex(
                name: "IX_Lecciones_modulo_id",
                table: "Leccion",
                newName: "IX_Leccion_ModuloID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leccion",
                table: "Leccion",
                column: "LeccionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Leccion_Modulos_ModuloID",
                table: "Leccion",
                column: "ModuloID",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
