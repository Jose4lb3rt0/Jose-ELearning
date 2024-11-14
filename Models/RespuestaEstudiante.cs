using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class RespuestaEstudiante
    {
        [Key]
        public int RespuestaID { get; set; }

        [ForeignKey("Cuestionario")]
        public int CuestionarioID { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioID { get; set; }

        [ForeignKey("Pregunta")]
        public int PreguntaID { get; set; }

        [ForeignKey("OpcionPregunta")]
        public int? OpcionID { get; set; }

        public string RespuestaTexto { get; set; }

        public DateTime FechaRespuesta { get; set; } = DateTime.Now;

        public Cuestionario Cuestionario { get; set; }
        public ApplicationUser AplicacionUsuario { get; set; }
        public Pregunta Pregunta { get; set; }
        public OpcionPregunta OpcionPregunta { get; set; }
    }
}
