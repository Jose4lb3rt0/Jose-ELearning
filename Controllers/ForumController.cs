using E_Platform.Data;
using E_Platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Platform.Controllers
{
    [Route("Forum/{cursoId}")]
    public class ForumController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForumController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Listar posts del curso
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index(int cursoId)
        {
            var posts = await _context.Posts
                .Include(p => p.Comentarios)
                .Where(p => p.CursoID == cursoId)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();

            ViewBag.CursoID = cursoId;
            return View(posts);
        }

        // Mostrar la vista para crear un nuevo post
        [HttpGet]
        [Route("Create")]
        public IActionResult Create(int cursoId)
        {
            ViewBag.CursoID = cursoId;
            return View(cursoId); // Pasamos el cursoId al modelo de la vista
        }

        // Crear un nuevo post
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cursoId, string titulo, string contenido)
        {
            var usuarioId = _userManager.GetUserId(User);
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(contenido))
            {
                TempData["ErrorMessage"] = "El título y contenido son obligatorios.";
                return RedirectToAction(nameof(Index), new { cursoId });
            }

            var post = new Post
            {
                CursoID = cursoId,
                Titulo = titulo,
                Contenido = contenido,
                UsuarioID = usuarioId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Publicación creada con éxito.";
            return RedirectToAction(nameof(Index), new { cursoId });
        }

        // Ver detalles de un post
        [HttpGet]
        [Route("Post/{postId}")]
        public async Task<IActionResult> ViewPost(int cursoId, int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Comentarios)
                .FirstOrDefaultAsync(p => p.PostID == postId && p.CursoID == cursoId);

            if (post == null) return NotFound();

            ViewBag.CursoID = cursoId;
            return View(post);
        }

        // Agregar un comentario
        [HttpPost]
        [Route("Post/{postId}/AddComment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int cursoId, int postId, string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
            {
                TempData["ErrorMessage"] = "El contenido del comentario no puede estar vacío.";
                return RedirectToAction(nameof(ViewPost), new { cursoId, postId });
            }

            var usuarioId = _userManager.GetUserId(User);
            var comentario = new Comment
            {
                PostID = postId,
                Contenido = contenido,
                UsuarioID = usuarioId,
                FechaCreacion = DateTime.Now
            };

            _context.Comments.Add(comentario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Comentario agregado con éxito.";
            return RedirectToAction(nameof(ViewPost), new { cursoId, postId });
        }
    }

}
