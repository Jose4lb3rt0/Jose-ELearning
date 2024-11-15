namespace E_Platform.Models
{
    public class CursoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public decimal Precio { get; set; }
        public string InstructorName { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double ProgresoCurso { get; set; }
    }
}
