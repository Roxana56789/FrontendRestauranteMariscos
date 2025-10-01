using Microsoft.AspNetCore.Mvc;
using FrontendRestauranteMarisco.WebApp.DTOs.CategoriaDTOS;
using FrontendRestauranteMarisco.WebApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace FrontendRestauranteMarisco.WebApp.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
            => _categoriaService = categoriaService;

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var categoria = await _categoriaService.GetAllAsync(token);
                return View(categoria);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudieron listar las categorias: " + ex.Message;
                return View(new List<CategoriaRespuestaDTO>());
            }
        }

        // GET: Detalles
        public async Task<IActionResult> Details(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var categoria = await _categoriaService.GetByIdAsync(id, token);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // GET: Crear
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaCreateDTO categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var categoria2 = await _categoriaService.CreateAsync(categoria, token);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la categoria: " + ex.Message);
                return View(categoria);
            }
        }

        // GET: Editar
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var cargo = await _categoriaService.GetByIdAsync(id, token);
            if (cargo == null)
            {
                return NotFound();
            }

            var dto = new CategoriaActualizarDTO
            {
                Nombre = cargo.Nombre,
                Descripcion = cargo.Descripcion
            };
            return View(dto);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaActualizarDTO categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var token = await HttpContext.GetTokenAsync("access_token");
            var success = await _categoriaService.UpdateAsync(id, categoria, token);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al actualizar la categoria.");
            return View(categoria);
        }

        // GET: Eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var categoria = await _categoriaService.GetByIdAsync(id, token);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var success = await _categoriaService.DeleteAsync(id, token);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al eliminar la categoria.");
            return View("Delete", await _categoriaService.GetByIdAsync(id, token));
        }
    }
}
