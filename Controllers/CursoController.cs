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
using E_Platform.Services;
using System.Security.Claims;

namespace E_Platform.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class CursoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CursoController> _logger;
        private readonly PermissionService _permissionService;

        public CursoController(
            AppDbContext context, 
            UserManager<ApplicationUser> userManager, 
            ILogger<CursoController> logger, 
            PermissionService permissionService
        )
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);

            if (user == null)
            {
                return Unauthorized();
            }

            if (!await _permissionService.HasPermissionAsync(user, "VerCursos"))
            {
                return Forbid();
            }

            var permisos = await _permissionService.GetPermissionAsync(user);

            List<Curso> cursos = new List<Curso>();

            if (User.Identity.IsAuthenticated)
            {
                if (permisos.Contains("VerCursos"))
                { 
                    if (User.IsInRole("Administrador"))
                    {
                        cursos = await _context.Cursos
                            .Include(c => c.Objetivos)
                            .Include(c => c.Requisitos)
                            .Include(c => c.Instructor)
                            .ToListAsync();
                    }
                    else if (User.IsInRole("Alumno"))
                    {
                        cursos = await _context.Inscripciones
                            .Where(i => i.StudentId == userId)
                            .Include(i => i.Curso)
                            .ThenInclude(c => c.Instructor)
                            .Select(i => i.Curso)
                            .ToListAsync();
                    }
                }
            }

            ViewBag.Permisos = permisos;
            return View(cursos);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioId == null) return Unauthorized();

            var curso = await _context.Cursos
                .Include(c => c.Modulos)
                    .ThenInclude(m => m.Lecciones)
                        .ThenInclude(l => l.Progresos)
                .Include(c => c.Objetivos)
                .Include(c => c.Requisitos)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
            {
                return NotFound(new { message = "Curso no encontrado" }); 
            }

            //Quiero traerme el progreso
            var totalLecciones = curso.Modulos.SelectMany(m => m.Lecciones).Count();
            var leccionesCompletadas = curso.Modulos
                .SelectMany(m => m.Lecciones)
                .Count(l => l.Progresos.Any(p => p.UsuarioID == usuarioId && p.Completado));

            var progresoCurso = totalLecciones > 0 
                ? Math.Round((decimal)leccionesCompletadas / totalLecciones * 100, 2) 
                : 0;

            return Json(new
            {
                curso.Nombre,
                curso.Descripcion,
                Progreso = progresoCurso,
                Instructor = curso.Instructor?.Name ?? "Instructor no asignado",
                Objetivos = curso.Objetivos?.Select(o => o.Descripcion) ?? Enumerable.Empty<string>(),
                Requisitos = curso.Requisitos?.Select(r => r.Descripcion) ?? Enumerable.Empty<string>(),
                Modulos = curso.Modulos?.Select(m => new
                {
                    m.Id,
                    m.Titulo,
                    m.Descripcion,
                    Lecciones = m.Lecciones?.Select(l => new
                    {
                        l.LeccionID,
                        l.Nombre,
                        l.Contenido
                    }) ?? Enumerable.Empty<object>()
                }) ?? Enumerable.Empty<object>()
            });
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!await _permissionService.HasPermissionAsync(user, "CrearCurso"))
            { 
                return Forbid();
            }

            var instructors = await _userManager.GetUsersInRoleAsync("Instructor");
            var instructorsSelectList = instructors.Select(i => new { i.Id, i.Name }).ToList();

            ViewBag.Instructors = new SelectList(instructorsSelectList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nombre,Descripcion,InstructorId,Precio")] Curso curso,
            List<string> objetivos,
            List<string> requisitos,
            List<ModuloViewModel> modulos
        )
        {
            _logger.LogInformation("Datos del formulario:");
            foreach (var key in Request.Form.Keys)
            {
                _logger.LogInformation($"{key}: {Request.Form[key]}");
            }

            _logger.LogInformation($"Iniciando la creación de un nuevo curso: Nombre={curso.Nombre}, Descripción={curso.Descripcion}, InstructorId={curso.InstructorId}, Precio={curso.Precio}");

            if (ModelState.IsValid)
            {
                foreach (var objetivo in objetivos ?? Enumerable.Empty<string>())
                {
                    if (!string.IsNullOrWhiteSpace(objetivo))
                    {
                        curso.Objetivos.Add(new ObjetivoCurso { Descripcion = objetivo });
                        _logger.LogInformation($"Objetivo añadido: {objetivo}");
                    }
                }

                foreach (var requisito in requisitos ?? Enumerable.Empty<string>())
                {
                    if (!string.IsNullOrWhiteSpace(requisito))
                    {
                        curso.Requisitos.Add(new RequisitoCurso { Descripcion = requisito });
                        _logger.LogInformation($"Requisito añadido: {requisito}");
                    }
                }

                foreach (var modulo in modulos ?? Enumerable.Empty<ModuloViewModel>())
                {
                    if (!string.IsNullOrWhiteSpace(modulo.Titulo) && !string.IsNullOrWhiteSpace(modulo.Descripcion))
                    {
                        curso.Modulos.Add(new Modulo { Titulo = modulo.Titulo, Descripcion = modulo.Descripcion });
                        _logger.LogInformation($"Módulo añadido: Título={modulo.Titulo}, Descripción={modulo.Descripcion}");
                    }
                    else
                    {
                        _logger.LogWarning($"Módulo ignorado: Título={modulo.Titulo}, Descripción={modulo.Descripcion}");
                    }
                }

                _context.Add(curso);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Curso creado con éxito: {curso.Nombre}");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("El modelo del curso no es válido.");
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!await _permissionService.HasPermissionAsync(user, "EditarCurso"))
            {
                return Forbid();
            }

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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!await _permissionService.HasPermissionAsync(user, "EliminarCurso"))
            {
                return Forbid();
            }

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
