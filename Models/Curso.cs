using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_Platform.Models
    
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public string InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser Instructor { get; set; }

        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        public ICollection<Modulo> Modulos { get; set; }
        public ICollection<ObjetivoCurso> Objetivos { get; set; }
        public ICollection<RequisitoCurso> Requisitos { get; set; }
    }
}
