using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

namespace E_Platform.Models
{
    public class Modulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [ForeignKey("Curso")]
        public int CursoId { get; set; }

        public Curso Curso { get; set; }
        public ICollection<Leccion> Lecciones { get; set; }
    }
}
