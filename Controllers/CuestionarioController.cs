using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Platform.Data;
using E_Platform.Models;
using System.Security.Claims;

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

        [HttpGet]
        public async Task<IActionResult> Responder(int id)
        {
            var cuestionario = await _context.Cuestionarios
                .Include(c => c.Preguntas)
                .ThenInclude(p => p.Opciones)
                .FirstOrDefaultAsync(c => c.CuestionarioID == id);

            if (cuestionario == null)
            {
                return NotFound(new { message = "Cuestionario no encontrado" });
            }

            return View(cuestionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Responder(IFormCollection form)
        {
            try
            {
                _logger.LogInformation("Datos enviados en el formulario:");
                foreach (var key in form.Keys)
                {
                    _logger.LogInformation($"{key}: {form[key]}");
                }

                // Obtener CuestionarioID
                if (!form.TryGetValue("CuestionarioID", out var cuestionarioIdValue) ||
                    !int.TryParse(cuestionarioIdValue, out int cuestionarioId))
                {
                    _logger.LogError("CuestionarioID no es válido o no está presente en el formulario.");
                    TempData["ErrorMessage"] = "El ID del cuestionario no es válido.";
                    return RedirectToAction("Index", "Cuestionario");
                }

                // Obtener usuario actual
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var leccionId = _context.Cuestionarios
                    .Where(c => c.CuestionarioID == cuestionarioId)
                    .Select(c => c.LeccionID)
                    .FirstOrDefault();

                if (usuarioId == null)
                {
                    return Unauthorized();
                }

                // Procesar respuestas y calcular calificación
                int totalPreguntas = 0;
                int respuestasCorrectas = 0;

                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("Respuestas["))
                    {
                        var preguntaIdString = key.Substring(11, key.Length - 12);
                        if (!int.TryParse(preguntaIdString, out int preguntaId))
                        {
                            _logger.LogWarning($"PreguntaID no válida: {preguntaIdString}");
                            continue;
                        }

                        var opcionSeleccionada = form[key];
                        var opcionCorrecta = await _context.OpcionesPreguntas
                            .Where(o => o.PreguntaID == preguntaId && o.EsCorrecta == true)
                            .Select(o => o.OpcionID.ToString())
                            .FirstOrDefaultAsync();

                        if (opcionSeleccionada == opcionCorrecta)
                        {
                            respuestasCorrectas++;
                        }
                        totalPreguntas++;
                    }
                }

                // Calcular la puntuación
                decimal puntuacion = totalPreguntas > 0
                    ? Math.Round((decimal)respuestasCorrectas / totalPreguntas * 20, 2)
                    : 0;

                _logger.LogInformation($"Puntuación calculada: {puntuacion}");

                // Guardar la calificación en la base de datos
                var calificacion = new Calificacion
                {
                    CuestionarioID = cuestionarioId,
                    UsuarioID = usuarioId,
                    Puntuacion = puntuacion,
                    FechaCalificacion = DateTime.Now
                };

                _context.Calificaciones.Add(calificacion);

                //---MODIFICACIÓN DEL PROGRESO---
                    var progresoCuestionario = await _context.Progresos.FirstOrDefaultAsync(p => p.UsuarioID == usuarioId && p.CuestionarioID == cuestionarioId);
                    
                    if(progresoCuestionario == null)
                    {  
                        var progreso = new Progreso
                        {
                            UsuarioID = usuarioId,
                            CursoID = _context.Lecciones.Where(l => l.LeccionID == leccionId).Select(l => l.Modulo.CursoId).FirstOrDefault(),
                            ModuloID = _context.Lecciones.Where(l => l.LeccionID == leccionId).Select(l => l.ModuloID).FirstOrDefault(),
                            LeccionID = leccionId,
                            CuestionarioID = cuestionarioId,
                            Completado = true,
                            FechaCompletado = DateTime.Now
                        };

                        _context.Progresos.Add(progreso);
                    }
                //-------------------------------
                
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Respuestas enviadas exitosamente. Tu calificación es: {puntuacion}/20.";
                return RedirectToAction("Details", "Cuestionario", new { id = cuestionarioId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar las respuestas del cuestionario.");
                TempData["ErrorMessage"] = "Hubo un error al procesar tus respuestas. Por favor, intenta de nuevo.";
                return RedirectToAction("Responder", new { id = form["CuestionarioID"] });
            }
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
                if(ModelState.IsValid)
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
