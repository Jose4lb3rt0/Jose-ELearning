using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public string UsuarioID { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Post Post { get; set; }
    }
}
