using E_Platform.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Platform.Services
{
    public class ProgresoService : IProgresoService
    {
        private readonly AppDbContext _context;

        public ProgresoService(AppDbContext context)
        {  
            _context = context; 
        }

        public async Task<double> CalcularProgresoCursoAsync(string usuarioId, int cursoId)
        {
            var totalLecciones = await _context.Lecciones
                .Where(l => l.Modulo.CursoId == cursoId)
                .CountAsync();

            var leccionesCompletadas = await _context.Progresos
                .Where(p => p.UsuarioID == usuarioId && p.CursoID == cursoId && p.Completado)
                .CountAsync();

            return totalLecciones > 0
                ? (double)leccionesCompletadas / totalLecciones * 100
                : 0;
        }

        public async Task<double> CalcularProgresoModuloAsync(string usuarioId, int moduloId)
        {
            var totalLecciones = await _context.Lecciones
                .Where(l => l.ModuloID == moduloId)
                .CountAsync();

            var leccionesCompletadas = await _context.Progresos
                .Where(p => p.UsuarioID == usuarioId && p.ModuloID == moduloId && p.Completado)
                .CountAsync();

            return totalLecciones > 0
                ? (double)leccionesCompletadas / totalLecciones * 100
                : 0;
        }
    }
}
