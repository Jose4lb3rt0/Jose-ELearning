using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Calificacion
    {
        [Key]
        public int CalificacionID { get; set; }

        [ForeignKey("Cuestionario")]
        public int CuestionarioID { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioID { get; set; }

        [Column("Puntuacion", TypeName = "decimal(5, 2)")]
        [Required]
        [Range(0, 20)] // Ajustado a un rango de 0 a 20
        public decimal Puntuacion { get; set; }

        public DateTime FechaCalificacion { get; set; } = DateTime.Now;

        public Cuestionario Cuestionario { get; set; }
        public ApplicationUser AplicacionUsuario { get; set; }
    }

}
