namespace E_Platform.Models
{
    public class ResponderCuestionarioViewModel
    {
        public int CuestionarioId { get; set; }
        public Dictionary<int, int?> Respuestas { get; set; } = new Dictionary<int, int?>();
        public Dictionary<int, string> RespuestasTexto { get; set; } = new Dictionary<int, string>();
    }
}
