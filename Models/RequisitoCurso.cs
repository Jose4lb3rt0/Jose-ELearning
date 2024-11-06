using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class RequisitoCurso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [ForeignKey("Curso")]
        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}
