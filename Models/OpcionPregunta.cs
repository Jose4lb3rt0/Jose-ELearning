using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class OpcionPregunta
    {
        [Key]
        public int OpcionID { get; set; }

        [ForeignKey("Pregunta")]
        public int PreguntaID { get; set; }

        public string TextoOpcion { get; set; }

        [Column("es_correcta")]
        public bool? EsCorrecta { get; set; } = false;

        public Pregunta? Pregunta { get; set; }
        public ICollection<RespuestaEstudiante>? RespuestasEstudiantes { get; set; }
    }
}
