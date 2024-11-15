using System.Security.Claims;
using E_Platform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Platform.Controllers
{
    public class ModuloController : Controller
    {
        private readonly AppDbContext _context;

        // GET: ModuloController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ModuloController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null) return Unauthorized();

            var modulo = await _context.Modulos
                .Include(m => m.Lecciones)
                    .ThenInclude(l => l.Cuestionarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (modulo == null) return NotFound();

            var lecciones = modulo.Lecciones.ToList();
            var leccionesCompletadas = lecciones
                .Count(l => _context.Cuestionarios
                    .Where(c => c.LeccionID == l.LeccionID)
                    .All(c => _context.Progresos.Any(p => p.CuestionarioID == c.CuestionarioID && p.Completado)));

            var progresoModulo = lecciones.Count > 0
                ? Math.Round((decimal)leccionesCompletadas/lecciones.Count * 100, 2)
                : 0;

            ViewBag.ProgresoModulo = progresoModulo;
            return View(modulo);
        }

        // GET: ModuloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModuloController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModuloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModuloController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
