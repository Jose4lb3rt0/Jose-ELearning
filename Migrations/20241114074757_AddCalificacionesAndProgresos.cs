using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddCalificacionesAndProgresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "es_correcta",
                table: "OpcionesPreguntas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    CalificacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuestionarioID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Puntuacion = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Retroalimentacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCalificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.CalificacionID);
                    table.ForeignKey(
                        name: "FK_Calificaciones_AspNetUsers_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Cuestionarios_CuestionarioID",
                        column: x => x.CuestionarioID,
                        principalTable: "Cuestionarios",
                        principalColumn: "CuestionarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresos",
                columns: table => new
                {
                    ProgresoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false),
                    ModuloID = table.Column<int>(type: "int", nullable: false),
                    LeccionID = table.Column<int>(type: "int", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresos", x => x.ProgresoID);
                    table.ForeignKey(
                        name: "FK_Progresos_AspNetUsers_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progresos_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progresos_Lecciones_LeccionID",
                        column: x => x.LeccionID,
                        principalTable: "Lecciones",
                        principalColumn: "leccion_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progresos_Modulos_ModuloID",
                        column: x => x.ModuloID,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CuestionarioID",
                table: "Calificaciones",
                column: "CuestionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioID",
                table: "Calificaciones",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresos_CursoID",
                table: "Progresos",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresos_LeccionID",
                table: "Progresos",
                column: "LeccionID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresos_ModuloID",
                table: "Progresos",
                column: "ModuloID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresos_UsuarioID",
                table: "Progresos",
                column: "UsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Progresos");

            migrationBuilder.AlterColumn<bool>(
                name: "es_correcta",
                table: "OpcionesPreguntas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
