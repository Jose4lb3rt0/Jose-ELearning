using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Leccion
    {
        [Key]
        public int LeccionID { get; set; }

        [ForeignKey("Modulo")]
        public int ModuloID { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Column("contenido")]
        public string Contenido { get; set; }

        public Modulo Modulo { get; set; }
        //public ICollection<Cuestionario> Cuestionarios { get; set; }
        //public ICollection<Archivo> Archivos { get; set; }
    }
}
