using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Platform.Models
{
    public class Inscripcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }

        [Required]
        public int CursoId { get; set; }

        [ForeignKey("CursoId")]
        public Curso Curso { get; set; }

        public DateTime FechaInscripcion { get; set; } = DateTime.Now;
    }
}
