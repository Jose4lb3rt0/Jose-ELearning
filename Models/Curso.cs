using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_Platform.Models
    
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Es necesario asignar un instructor.")]
        public string InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser? Instructor { get; set; }


        [Required(ErrorMessage = "Debe especificar un precio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        [NotMapped]
        public double? ProgresoCurso { get; set; }

        public ICollection<Modulo>? Modulos { get; set; } = new List<Modulo>();
        public ICollection<ObjetivoCurso>? Objetivos { get; set; } = new List<ObjetivoCurso>();
        public ICollection<RequisitoCurso>? Requisitos { get; set; } = new List<RequisitoCurso>();
    }
}
