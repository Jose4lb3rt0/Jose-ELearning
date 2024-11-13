using E_Platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_Platform.Data
{
    //public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<ObjetivoCurso> ObjetivosCurso { get; set; }
        public DbSet<RequisitoCurso> RequisitosCurso { get; set; }
        public DbSet<Inscripcion> Inscripciones {  get; set; }
        public DbSet<AppPermission> AppPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Leccion> Lecciones { get; set; }
        public DbSet<Cuestionario> Cuestionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().ToTable("AspNetRoles");

            builder.Entity<ApplicationUser>()
                .Property(a => a.EstudiaCoins)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Curso>()
                .HasOne(c => c.Instructor)
                .WithMany()
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Curso>()
                .Property(c => c.Precio)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Curso>()
                .HasMany(c => c.Modulos)
                .WithOne(m => m.Curso)
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany()
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            builder.Entity<Leccion>()
                .HasOne(l => l.Modulo)
                .WithMany(m => m.Lecciones)
                .HasForeignKey(l => l.ModuloID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cuestionario>()
                .HasOne(c => c.Leccion)
                .WithMany(l => l.Cuestionarios)
                .HasForeignKey(c => c.LeccionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
