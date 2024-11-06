using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Platform.Data;
using E_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace E_Platform.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CursoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CursoController> _logger;

        public CursoController(AppDbContext context, UserManager<ApplicationUser> userManager, ILogger<CursoController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Objetivos)
                .Include(c => c.Requisitos)
                .Include(c => c.Instructor)
                .ToListAsync();

            return View(cursos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var instructors = await _userManager.GetUsersInRoleAsync("Instructor");

            var instructorSelectList = instructors.Select(i => new { i.Id, i.Name }).ToList();

            ViewBag.Instructors = new SelectList(instructorSelectList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,InstructorId,Precio")] Curso curso, List<string> objetivos, List<string> requisitos)
        {
            _logger.LogInformation($"Iniciando la creación de un nuevo curso: {curso}");

            if (ModelState.IsValid)
            {
                 // Agregar objetivos
                foreach (var objetivo in objetivos)
                {
                    if (!string.IsNullOrWhiteSpace(objetivo))
                    {
                        curso.Objetivos.Add(new ObjetivoCurso { Descripcion = objetivo });
                    }
                }

                // Agregar requisitos
                foreach (var requisito in requisitos)
                {
                    if (!string.IsNullOrWhiteSpace(requisito))
                    {
                        curso.Requisitos.Add(new RequisitoCurso { Descripcion = requisito });
                    }
                }
                _context.Add(curso);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Curso creado con éxito: {curso.Nombre}");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning($"El modelo del curso no es válido. Detalles de errores:");
            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;
                foreach (var error in errors)
                {
                    _logger.LogWarning($"Campo: {key}, Error: {error.ErrorMessage}");
                }
            }

            ViewBag.Instructors = new SelectList(await _userManager.GetUsersInRoleAsync("Instructor"), "Id", "Name");
            return View(curso);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,InstructorId,Precio,Activo")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            return View(curso);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
