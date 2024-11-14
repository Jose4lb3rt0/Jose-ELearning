using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Cuestionario
    {
        [Key]
        public int CuestionarioID { get; set; }

        [ForeignKey("Leccion")]
        public int LeccionID { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Leccion? Leccion { get; set; }
        public ICollection<Pregunta>? Preguntas { get; set; }
        public ICollection<RespuestaEstudiante>? RespuestasEstudiantes { get; set; }
    }
}
