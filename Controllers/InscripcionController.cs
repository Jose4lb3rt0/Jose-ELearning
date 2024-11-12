using E_Platform.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Platform.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Platform.Controllers
{
    [Authorize(Roles = "Alumno")]
    public class InscripcionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PaymentService _paymentService;

        public InscripcionController(AppDbContext context, UserManager<ApplicationUser> userManager, PaymentService paymentService)
        {
            _context = context;
            _userManager = userManager;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var cursosDisponibles = await _context.Cursos
                .Where(c => !_context.Inscripciones.Any(i => i.CursoId == c.Id && i.StudentId == userId))
                .ToListAsync();

            return View(cursosDisponibles);
        }

        [HttpPost]
        public async Task<IActionResult> Inscribir(int cursoId)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var curso = await _context.Cursos.FindAsync(cursoId);

            if (curso == null)
            {
                TempData["Error"] = "El curso no existe.";
                return RedirectToAction("Index");
            }

            var yaInscrito = await _context.Inscripciones
                .AnyAsync(i => i.CursoId == cursoId && i.StudentId == userId);
            if (yaInscrito)
            {
                TempData["Error"] = "Ya estás inscrito en este curso.";
                return RedirectToAction("Index");
            }

            if (await _paymentService.PurchaseCourse(user, curso))
            {
                var inscripcion = new Inscripcion
                {
                    StudentId = userId,
                    CursoId = cursoId
                };

                _context.Inscripciones.Add(inscripcion);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Te has inscrito exitosamente al curso.";
            }
            else
            {
                TempData["Error"] = "No tienes suficiente saldo para inscribirte en este curso.";
            }

            return RedirectToAction("Index");
        }

        //Este lo he movido al controlador de CursosController
        public async Task<IActionResult> MisCursos()
        {
            var userId = _userManager.GetUserId(User);

            var cursosInscritos = await _context.Inscripciones
                .Where(i => i.StudentId == userId)
                .Include(i => i.Curso) 
                .ThenInclude(c => c.Instructor) 
                .Select(i => i.Curso) 
                .ToListAsync();


            return View(cursosInscritos);
        }
    }
}
