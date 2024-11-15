
namespace E_Platform.Services
{
    public interface IProgresoService
    {
        Task<double> CalcularProgresoCursoAsync(string usuarioId, int cursoId);
        Task<double> CalcularProgresoModuloAsync(string usuarioId, int moduloId);
    }
}
