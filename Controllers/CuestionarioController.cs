using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Platform.Data;
using E_Platform.Models;

namespace E_Platform.Controllers
{
    public class CuestionarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CuestionarioController> _logger;

        public CuestionarioController(AppDbContext context, ILogger<CuestionarioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Cuestionario
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Cuestionarios.Include(c => c.Leccion);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Cuestionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios
                .Include(c => c.Preguntas)
                    .ThenInclude(p => p.Opciones)
                .FirstOrDefaultAsync(c => c.CuestionarioID == id);

            if (cuestionario == null)
            {
                return NotFound();
            }

            return View(cuestionario);
        }

        // GET: Cuestionario/Create
        [HttpGet]
        public IActionResult Create(int leccionId)
        {
            var cuestionario = new Cuestionario
            {
                LeccionID = leccionId,
                Preguntas = new List<Pregunta>()
            };
            ViewBag.LeccionID = new SelectList(_context.Lecciones, "LeccionID", "Nombre", leccionId);
            return View(cuestionario);
        }

        // POST: Cuestionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cuestionario cuestionario)
        {
            _logger.LogInformation("Iniciando la creación del cuestionario...");

            try
            {
                // Validar y convertir "on" a true
                foreach (var pregunta in cuestionario.Preguntas ?? new List<Pregunta>())
                {
                    foreach (var opcion in pregunta.Opciones ?? new List<OpcionPregunta>())
                    {
                        if (opcion.EsCorrecta == false && HttpContext.Request.Form[$"Preguntas[{pregunta.PreguntaID}].Opciones[{opcion.OpcionID}].EsCorrecta"] == "on")
                        {
                            opcion.EsCorrecta = true;
                        }
                    }
                }

                // Validar el modelo
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("ModelState es válido. Guardando cuestionario...");
                    _context.Add(cuestionario);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Cuestionario creado exitosamente.");
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogWarning("ModelState no es válido.");
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        _logger.LogWarning($"Campo: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el cuestionario.");
            }

            _logger.LogInformation("Preparando la vista para volver a mostrar el formulario.");
            ViewBag.LeccionID = new SelectList(_context.Lecciones, "LeccionID", "Nombre", cuestionario.LeccionID);
            return View(cuestionario);
        }

        // GET: Cuestionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios.FindAsync(id);
            if (cuestionario == null)
            {
                return NotFound();
            }
            ViewData["LeccionID"] = new SelectList(_context.Lecciones, "LeccionID", "Nombre", cuestionario.LeccionID);
            return View(cuestionario);
        }

        // POST: Cuestionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuestionarioID,LeccionID,Nombre,FechaCreacion")] Cuestionario cuestionario)
        {
            if (id != cuestionario.CuestionarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuestionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuestionarioExists(cuestionario.CuestionarioID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeccionID"] = new SelectList(_context.Lecciones, "LeccionID", "Nombre", cuestionario.LeccionID);
            return View(cuestionario);
        }

        // GET: Cuestionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios
                .Include(c => c.Leccion)
                .FirstOrDefaultAsync(m => m.CuestionarioID == id);
            if (cuestionario == null)
            {
                return NotFound();
            }

            return View(cuestionario);
        }

        // POST: Cuestionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuestionario = await _context.Cuestionarios.FindAsync(id);
            if (cuestionario != null)
            {
                _context.Cuestionarios.Remove(cuestionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuestionarioExists(int id)
        {
            return _context.Cuestionarios.Any(e => e.CuestionarioID == id);
        }
    }
}
