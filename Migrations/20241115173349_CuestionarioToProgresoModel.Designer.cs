﻿// <auto-generated />
using System;
using E_Platform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Platform.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241115173349_CuestionarioToProgresoModel")]
    partial class CuestionarioToProgresoModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_Platform.Models.AppPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppPermissions");
                });

            modelBuilder.Entity("E_Platform.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<decimal>("EstudiaCoins")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("E_Platform.Models.Calificacion", b =>
                {
                    b.Property<int>("CalificacionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalificacionID"));

                    b.Property<int>("CuestionarioID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCalificacion")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Puntuacion")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("Puntuacion");

                    b.Property<string>("UsuarioID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CalificacionID");

                    b.HasIndex("CuestionarioID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("E_Platform.Models.Cuestionario", b =>
                {
                    b.Property<int>("CuestionarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CuestionarioID"));

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeccionID")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("CuestionarioID");

                    b.HasIndex("LeccionID");

                    b.ToTable("Cuestionarios");
                });

            modelBuilder.Entity("E_Platform.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstructorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("E_Platform.Models.Inscripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("StudentId");

                    b.ToTable("Inscripciones");
                });

            modelBuilder.Entity("E_Platform.Models.Leccion", b =>
                {
                    b.Property<int>("LeccionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("leccion_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeccionID"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModuloID")
                        .HasColumnType("int")
                        .HasColumnName("modulo_id");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("LeccionID");

                    b.HasIndex("ModuloID");

                    b.ToTable("Lecciones");
                });

            modelBuilder.Entity("E_Platform.Models.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("E_Platform.Models.ObjetivoCurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("ObjetivosCurso");
                });

            modelBuilder.Entity("E_Platform.Models.OpcionPregunta", b =>
                {
                    b.Property<int>("OpcionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OpcionID"));

                    b.Property<bool?>("EsCorrecta")
                        .HasColumnType("bit")
                        .HasColumnName("es_correcta");

                    b.Property<int>("PreguntaID")
                        .HasColumnType("int");

                    b.Property<string>("TextoOpcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OpcionID");

                    b.HasIndex("PreguntaID");

                    b.ToTable("OpcionesPreguntas");
                });

            modelBuilder.Entity("E_Platform.Models.Pregunta", b =>
                {
                    b.Property<int>("PreguntaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreguntaID"));

                    b.Property<int>("CuestionarioID")
                        .HasColumnType("int");

                    b.Property<string>("TextoPregunta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPregunta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PreguntaID");

                    b.HasIndex("CuestionarioID");

                    b.ToTable("Preguntas");
                });

            modelBuilder.Entity("E_Platform.Models.Progreso", b =>
                {
                    b.Property<int>("ProgresoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgresoID"));

                    b.Property<bool>("Completado")
                        .HasColumnType("bit");

                    b.Property<int?>("CuestionarioID")
                        .HasColumnType("int");

                    b.Property<int>("CursoID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCompletado")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeccionID")
                        .HasColumnType("int");

                    b.Property<int>("ModuloID")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProgresoID");

                    b.HasIndex("CuestionarioID");

                    b.HasIndex("CursoID");

                    b.HasIndex("LeccionID");

                    b.HasIndex("ModuloID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Progresos");
                });

            modelBuilder.Entity("E_Platform.Models.RequisitoCurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("RequisitosCurso");
                });

            modelBuilder.Entity("E_Platform.Models.RespuestaEstudiante", b =>
                {
                    b.Property<int>("RespuestaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RespuestaID"));

                    b.Property<int>("CuestionarioID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRespuesta")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OpcionID")
                        .HasColumnType("int");

                    b.Property<int>("PreguntaID")
                        .HasColumnType("int");

                    b.Property<string>("RespuestaTexto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RespuestaID");

                    b.HasIndex("CuestionarioID");

                    b.HasIndex("OpcionID");

                    b.HasIndex("PreguntaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("RespuestasDeEstudiantes");
                });

            modelBuilder.Entity("E_Platform.Models.RolePermission", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("E_Platform.Models.Calificacion", b =>
                {
                    b.HasOne("E_Platform.Models.Cuestionario", "Cuestionario")
                        .WithMany("Calificaciones")
                        .HasForeignKey("CuestionarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.ApplicationUser", "AplicacionUsuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AplicacionUsuario");

                    b.Navigation("Cuestionario");
                });

            modelBuilder.Entity("E_Platform.Models.Cuestionario", b =>
                {
                    b.HasOne("E_Platform.Models.Leccion", "Leccion")
                        .WithMany("Cuestionarios")
                        .HasForeignKey("LeccionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leccion");
                });

            modelBuilder.Entity("E_Platform.Models.Curso", b =>
                {
                    b.HasOne("E_Platform.Models.ApplicationUser", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("E_Platform.Models.Inscripcion", b =>
                {
                    b.HasOne("E_Platform.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("E_Platform.Models.Leccion", b =>
                {
                    b.HasOne("E_Platform.Models.Modulo", "Modulo")
                        .WithMany("Lecciones")
                        .HasForeignKey("ModuloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("E_Platform.Models.Modulo", b =>
                {
                    b.HasOne("E_Platform.Models.Curso", "Curso")
                        .WithMany("Modulos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("E_Platform.Models.ObjetivoCurso", b =>
                {
                    b.HasOne("E_Platform.Models.Curso", "Curso")
                        .WithMany("Objetivos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("E_Platform.Models.OpcionPregunta", b =>
                {
                    b.HasOne("E_Platform.Models.Pregunta", "Pregunta")
                        .WithMany("Opciones")
                        .HasForeignKey("PreguntaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("E_Platform.Models.Pregunta", b =>
                {
                    b.HasOne("E_Platform.Models.Cuestionario", "Cuestionario")
                        .WithMany("Preguntas")
                        .HasForeignKey("CuestionarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuestionario");
                });

            modelBuilder.Entity("E_Platform.Models.Progreso", b =>
                {
                    b.HasOne("E_Platform.Models.Cuestionario", "Cuestionario")
                        .WithMany()
                        .HasForeignKey("CuestionarioID");

                    b.HasOne("E_Platform.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.Leccion", "Leccion")
                        .WithMany("Progresos")
                        .HasForeignKey("LeccionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.Modulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.ApplicationUser", "AplicacionUsuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AplicacionUsuario");

                    b.Navigation("Cuestionario");

                    b.Navigation("Curso");

                    b.Navigation("Leccion");

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("E_Platform.Models.RequisitoCurso", b =>
                {
                    b.HasOne("E_Platform.Models.Curso", "Curso")
                        .WithMany("Requisitos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("E_Platform.Models.RespuestaEstudiante", b =>
                {
                    b.HasOne("E_Platform.Models.Cuestionario", "Cuestionario")
                        .WithMany("RespuestasEstudiantes")
                        .HasForeignKey("CuestionarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.OpcionPregunta", "OpcionPregunta")
                        .WithMany("RespuestasEstudiantes")
                        .HasForeignKey("OpcionID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("E_Platform.Models.Pregunta", "Pregunta")
                        .WithMany("RespuestasEstudiantes")
                        .HasForeignKey("PreguntaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.ApplicationUser", "AplicacionUsuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AplicacionUsuario");

                    b.Navigation("Cuestionario");

                    b.Navigation("OpcionPregunta");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("E_Platform.Models.RolePermission", b =>
                {
                    b.HasOne("E_Platform.Models.AppPermission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("E_Platform.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("E_Platform.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Platform.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("E_Platform.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("E_Platform.Models.Cuestionario", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("Preguntas");

                    b.Navigation("RespuestasEstudiantes");
                });

            modelBuilder.Entity("E_Platform.Models.Curso", b =>
                {
                    b.Navigation("Modulos");

                    b.Navigation("Objetivos");

                    b.Navigation("Requisitos");
                });

            modelBuilder.Entity("E_Platform.Models.Leccion", b =>
                {
                    b.Navigation("Cuestionarios");

                    b.Navigation("Progresos");
                });

            modelBuilder.Entity("E_Platform.Models.Modulo", b =>
                {
                    b.Navigation("Lecciones");
                });

            modelBuilder.Entity("E_Platform.Models.OpcionPregunta", b =>
                {
                    b.Navigation("RespuestasEstudiantes");
                });

            modelBuilder.Entity("E_Platform.Models.Pregunta", b =>
                {
                    b.Navigation("Opciones");

                    b.Navigation("RespuestasEstudiantes");
                });
#pragma warning restore 612, 618
        }
    }
}