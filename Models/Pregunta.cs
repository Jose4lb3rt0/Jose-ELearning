using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Pregunta
    {
        [Key]
        public int PreguntaID { get; set; }

        [ForeignKey("Cuestionario")]
        public int CuestionarioID { get; set; }

        [Required]
        public string TextoPregunta { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoPregunta { get; set; }

        public Cuestionario? Cuestionario { get; set; }
        public ICollection<OpcionPregunta>? Opciones { get; set; }
        public ICollection<RespuestaEstudiante>? RespuestasEstudiantes { get; set; }
    }
}
