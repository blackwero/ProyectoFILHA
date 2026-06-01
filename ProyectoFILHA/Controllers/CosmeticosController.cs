using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;
using ProyectoFILHA.Services;

namespace ProyectoFILHA.Controllers
{
    public class CosmeticosController : Controller
    {
        private readonly CosmeticoService _cosmeticoService;
        private readonly CategoriaService _categoriaService;
        private readonly PresentacionService _presentacionService;

        public CosmeticosController(
            CosmeticoService cosmeticoService,
            CategoriaService categoriaService,
            PresentacionService presentacionService)
        {
            _cosmeticoService = cosmeticoService;
            _categoriaService = categoriaService;
            _presentacionService = presentacionService;
        }

        private async Task CargarCombos()
        {
            var categorias =
                await _categoriaService.ObtenerTodos();

            var presentaciones =
                await _presentacionService.ObtenerTodos();

            ViewBag.Categorias = new SelectList(
                categorias.Where(c => c.Estado == EstadoEnum.Activo),
                "Id",
                "Nombre");

            ViewBag.Presentaciones = new SelectList(
                presentaciones.Where(p => p.Estado == EstadoEnum.Activo),
                "Id",
                "Nombre");
        }

        public async Task<IActionResult> AdminIndex(
            string buscar,
            int? categoriaId,
            int? presentacionId,
            decimal? precioMin,
            decimal? precioMax,
            int? esVegano,
            int? esDermatologico,
            EstadoEnum? estado,
            int page = 1)
        {
            int pageSize = 10;

            var lista =
                await _cosmeticoService.ObtenerTodos();

            if (!string.IsNullOrEmpty(buscar))
            {
                lista = lista
                    .Where(c => c.Nombre != null &&
                           c.Nombre.Contains(
                               buscar,
                               StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (categoriaId.HasValue)
                lista = lista
                    .Where(c => c.CategoriaId == categoriaId)
                    .ToList();

            if (presentacionId.HasValue)
                lista = lista
                    .Where(c => c.PresentacionId == presentacionId)
                    .ToList();

            if (precioMin.HasValue)
                lista = lista
                    .Where(c => c.Precio >= precioMin)
                    .ToList();

            if (precioMax.HasValue)
                lista = lista
                    .Where(c => c.Precio <= precioMax)
                    .ToList();

            if (esVegano.HasValue)
                lista = lista
                    .Where(c => c.EsVegano == esVegano)
                    .ToList();

            if (esDermatologico.HasValue)
                lista = lista
                    .Where(c => c.EsDermatologico == esDermatologico)
                    .ToList();

            if (estado.HasValue)
                lista = lista
                    .Where(c => c.Estado == estado.Value)
                    .ToList();

            int totalItems = lista.Count;

            lista = lista
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Categorias =
                await _categoriaService.ObtenerTodos();

            ViewBag.Presentaciones =
                await _presentacionService.ObtenerTodos();

            ViewBag.Buscar = buscar;
            ViewBag.CategoriaId = categoriaId;
            ViewBag.PresentacionId = presentacionId;
            ViewBag.PrecioMin = precioMin;
            ViewBag.PrecioMax = precioMax;
            ViewBag.EsVegano = esVegano;
            ViewBag.EsDermatologico = esDermatologico;
            ViewBag.Estado = estado;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages =
                (int)Math.Ceiling((double)totalItems / pageSize);

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCombos();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CosmeticoViewModel? cosmetico)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(cosmetico);
            }

            await _cosmeticoService.Crear(cosmetico);

            return RedirectToAction(nameof(AdminIndex));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cosmetico =
                await _cosmeticoService.ObtenerPorId(id);

            if (cosmetico == null)
                return NotFound();

            await CargarCombos();

            return View(cosmetico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CosmeticoViewModel cosmetico)
        {
            if (id != cosmetico.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(cosmetico);
            }

            await _cosmeticoService.Actualizar(
                id,
                cosmetico);

            return RedirectToAction(nameof(AdminIndex));
        }
    }
}