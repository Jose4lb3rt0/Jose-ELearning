using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Leccion
    {
        [Key]
        [Column("leccion_id")]
        public int LeccionID { get; set; }

        [ForeignKey("Modulo")]
        [Column("modulo_id")]
        [Required] 
        public int ModuloID { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public string Contenido { get; set; }

        public virtual Modulo? Modulo { get; set; }
        public virtual ICollection<Cuestionario>? Cuestionarios { get; set; } = new List<Cuestionario>();
        public virtual ICollection<Progreso>? Progresos { get; set; } = new List<Progreso>();
    }

}
