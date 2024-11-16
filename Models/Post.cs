using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [ForeignKey("Curso")]
        public int CursoID { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public string UsuarioID { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Curso Curso { get; set; }
        public ICollection<Comment> Comentarios { get; set; } = new List<Comment>();
    }
}
