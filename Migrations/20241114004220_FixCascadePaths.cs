using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuestionarios_Lecciones_leccion_id",
                table: "Cuestionarios");

            migrationBuilder.RenameColumn(
                name: "leccion_id",
                table: "Cuestionarios",
                newName: "LeccionID");

            migrationBuilder.RenameColumn(
                name: "cuestionario_id",
                table: "Cuestionarios",
                newName: "CuestionarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Cuestionarios_leccion_id",
                table: "Cuestionarios",
                newName: "IX_Cuestionarios_LeccionID");

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    PreguntaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuestionarioID = table.Column<int>(type: "int", nullable: false),
                    TextoPregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPregunta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.PreguntaID);
                    table.ForeignKey(
                        name: "FK_Preguntas_Cuestionarios_CuestionarioID",
                        column: x => x.CuestionarioID,
                        principalTable: "Cuestionarios",
                        principalColumn: "CuestionarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesPreguntas",
                columns: table => new
                {
                    OpcionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaID = table.Column<int>(type: "int", nullable: false),
                    TextoOpcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    es_correcta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesPreguntas", x => x.OpcionID);
                    table.ForeignKey(
                        name: "FK_OpcionesPreguntas_Preguntas_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "Preguntas",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasDeEstudiantes",
                columns: table => new
                {
                    RespuestaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuestionarioID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreguntaID = table.Column<int>(type: "int", nullable: false),
                    OpcionID = table.Column<int>(type: "int", nullable: true),
                    RespuestaTexto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasDeEstudiantes", x => x.RespuestaID);
                    table.ForeignKey(
                        name: "FK_RespuestasDeEstudiantes_AspNetUsers_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasDeEstudiantes_Cuestionarios_CuestionarioID",
                        column: x => x.CuestionarioID,
                        principalTable: "Cuestionarios",
                        principalColumn: "CuestionarioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasDeEstudiantes_OpcionesPreguntas_OpcionID",
                        column: x => x.OpcionID,
                        principalTable: "OpcionesPreguntas",
                        principalColumn: "OpcionID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RespuestasDeEstudiantes_Preguntas_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "Preguntas",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesPreguntas_PreguntaID",
                table: "OpcionesPreguntas",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_CuestionarioID",
                table: "Preguntas",
                column: "CuestionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDeEstudiantes_CuestionarioID",
                table: "RespuestasDeEstudiantes",
                column: "CuestionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDeEstudiantes_OpcionID",
                table: "RespuestasDeEstudiantes",
                column: "OpcionID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDeEstudiantes_PreguntaID",
                table: "RespuestasDeEstudiantes",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasDeEstudiantes_UsuarioID",
                table: "RespuestasDeEstudiantes",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuestionarios_Lecciones_LeccionID",
                table: "Cuestionarios",
                column: "LeccionID",
                principalTable: "Lecciones",
                principalColumn: "leccion_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuestionarios_Lecciones_LeccionID",
                table: "Cuestionarios");

            migrationBuilder.DropTable(
                name: "RespuestasDeEstudiantes");

            migrationBuilder.DropTable(
                name: "OpcionesPreguntas");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.RenameColumn(
                name: "LeccionID",
                table: "Cuestionarios",
                newName: "leccion_id");

            migrationBuilder.RenameColumn(
                name: "CuestionarioID",
                table: "Cuestionarios",
                newName: "cuestionario_id");

            migrationBuilder.RenameIndex(
                name: "IX_Cuestionarios_LeccionID",
                table: "Cuestionarios",
                newName: "IX_Cuestionarios_leccion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuestionarios_Lecciones_leccion_id",
                table: "Cuestionarios",
                column: "leccion_id",
                principalTable: "Lecciones",
                principalColumn: "leccion_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
