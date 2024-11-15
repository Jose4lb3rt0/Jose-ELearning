using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Progreso
    {
        [Key]
        public int ProgresoID { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioID { get; set; }

        [ForeignKey("Curso")]
        public int CursoID { get; set; }

        [ForeignKey("Modulo")]
        public int ModuloID { get; set; }

        [ForeignKey("Leccion")]
        public int LeccionID { get; set; }

        [ForeignKey("Cuestionario")]
        public int? CuestionarioID { get; set; }

        public bool Completado { get; set; } = false;

        public DateTime? FechaCompletado { get; set; }

        public ApplicationUser AplicacionUsuario { get; set; }
        public Curso Curso { get; set; }
        public Modulo Modulo { get; set; }
        public Leccion Leccion { get; set; }
        public Cuestionario? Cuestionario { get; set; }
    }

}
