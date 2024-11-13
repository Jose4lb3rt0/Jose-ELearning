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
    public class LeccionController : Controller
    {
        private readonly AppDbContext _context;

        public LeccionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lecciones.Include(l => l.Modulo);
            return View(await appDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson([FromBody] Leccion leccion)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new { Field = e.Key, Errors = e.Value.Errors.Select(x => x.ErrorMessage) });

                return Json(new { success = false, message = "Datos inválidos", errors });
            }

            // Agregar la lección al contexto
            _context.Lecciones.Add(leccion);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Lección agregada con éxito" });
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leccion = await _context.Lecciones
                .Include(l => l.Modulo)
                .FirstOrDefaultAsync(m => m.LeccionID == id);
            if (leccion == null)
            {
                return NotFound();
            }

            return View(leccion);
        }

        public IActionResult Create()
        {
            ViewData["ModuloID"] = new SelectList(_context.Modulos, "Id", "Titulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeccionID,ModuloID,Nombre,Contenido,TipoContenido,Orden")] Leccion leccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuloID"] = new SelectList(_context.Modulos, "Id", "Titulo", leccion.ModuloID);
            return View(leccion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leccion = await _context.Lecciones.FindAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }
            ViewData["ModuloID"] = new SelectList(_context.Modulos, "Id", "Titulo", leccion.ModuloID);
            return View(leccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeccionID,ModuloID,Nombre,Contenido,TipoContenido,Orden")] Leccion leccion)
        {
            if (id != leccion.LeccionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeccionExists(leccion.LeccionID))
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
            ViewData["ModuloID"] = new SelectList(_context.Modulos, "Id", "Titulo", leccion.ModuloID);
            return View(leccion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leccion = await _context.Lecciones
                .Include(l => l.Modulo)
                .FirstOrDefaultAsync(m => m.LeccionID == id);
            if (leccion == null)
            {
                return NotFound();
            }

            return View(leccion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leccion = await _context.Lecciones.FindAsync(id);
            if (leccion != null)
            {
                _context.Lecciones.Remove(leccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeccionExists(int id)
        {
            return _context.Lecciones.Any(e => e.LeccionID == id);
        }
    }
}
