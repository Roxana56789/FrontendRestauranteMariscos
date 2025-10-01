using FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs;
using FrontendRestauranteMarisco.WebApp.Service;
using FrontendRestauranteMarisco.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontendRestauranteMarisco.WebApp.Controllers
{
    [Authorize]
    public class PlatilloController : Controller
    {
        private readonly PlatilloService _platilloService;
        private readonly CategoriaService _categoriaService;

        public PlatilloController(PlatilloService platilloService, CategoriaService categoriaService)
        {
            _platilloService = platilloService;
            _categoriaService = categoriaService;
        }

        // GET: Proyecto
        public async Task<IActionResult> Index()
        {
            try
            {
                var platillos = await _platilloService.GetAllAsync();
                var categorias = await _categoriaService.GetAllAsync();
                var categoriaNombres = categorias.ToDictionary(m => m.Id, m => m.Nombre);
                ViewBag.CategoriaNombres = categoriaNombres;
                return View(platillos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudieron cargar los platillos: " + ex.Message;
                return View(new List<RespuestaPlatilloDTO>());
            }
        }


        // GET: Proyecto/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var platillos = await _platilloService.GetByIdAsync(id);
            var categorias = await _categoriaService.GetAllAsync();
            var categoriaNombres = categorias.ToDictionary(m => m.Id, m => m.Nombre);
            ViewBag.CategoriaNombres = categoriaNombres;
            if (platillos == null)
            {
                return NotFound();
            }
            return View(platillos);
        }

        // GET: Proyecto/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new CrearPlatilloDTO());
        }

        // POST: Proyecto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearPlatilloDTO platilloDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(platilloDto);
            }

            try
            {
                var nuevoProyecto = await _platilloService.CreateAsync(platilloDto, "tu_token_de_acceso");
                if (nuevoProyecto == null)
                {
                    ModelState.AddModelError("", "No se pudo crear el proyecto.");
                    await PopulateDropdowns();
                    return View(platilloDto);
                }
                TempData["Ok"] = "platillo creado con éxito.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el platillo: " + ex.Message);
                await PopulateDropdowns();
                return View(platilloDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var platillo = await _platilloService.GetByIdAsync(id);
            if (platillo == null)
            {
                return NotFound();
            }

            var dto = new ActualizarPlatilloDTO
            {
                Id = platillo.Id,
                Nombre = platillo.Nombre,
                Descripcion = platillo.Descripcion,
                Precio = platillo.Precio,
                CategoriaId = platillo.CategoriaId
            };
            await PopulateDropdowns();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarPlatilloDTO platillo)
        {

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(platillo);
            }
            var success = await _platilloService.UpdateAsync(id, platillo, "");
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Error al actualizar el platillo.");
            await PopulateDropdowns();
            return View(platillo);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var platillo = await _platilloService.GetByIdAsync(id);
            var categorias = await _categoriaService.GetAllAsync();
            var categoriaNombres = categorias.ToDictionary(m => m.Id, m => m.Nombre);
            ViewBag.CategoriaNombres = categoriaNombres;
            if (platillo == null)
            {
                return NotFound();
            }
            return View(platillo);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _platilloService.DeleteAsync(id, "tu_token_de_acceso");
                if (success)
                {
                    TempData["Ok"] = "platillo eliminado con éxito.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Error al eliminar el platillo.");
                return View("Delete", await _platilloService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el platillo: " + ex.Message);
                return View("Delete", await _platilloService.GetByIdAsync(id));
            }
        }

        // Mapeo del Dropdown 
        private async Task PopulateDropdowns()
        {
            var categorias = await _categoriaService.GetAllAsync();
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nombre");
        }
    }
}
