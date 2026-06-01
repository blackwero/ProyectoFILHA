using Microsoft.AspNetCore.Mvc;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;
using ProyectoFILHA.Services;

public class CategoriasController : Controller
{
    private readonly CategoriaService _service;

    public CategoriasController(
        CategoriaService service)
    {
        _service = service;
    }

    // LISTADO + FILTROS + PAGINACIÓN
    public async Task<IActionResult> Index(
        string buscar,
        EstadoEnum? estado,
        int page = 1)
    {
        int pageSize = 10;

        var lista = await _service.ObtenerTodos();

        if (!string.IsNullOrEmpty(buscar))
        {
            lista = lista
                .Where(c =>
                    c.Nombre != null &&
                    c.Nombre.Contains(
                        buscar,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (estado.HasValue)
        {
            lista = lista
                .Where(c => c.Estado == estado.Value)
                .ToList();
        }

        int totalItems = lista.Count();

        lista = lista
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages =
            (int)Math.Ceiling((double)totalItems / pageSize);

        ViewBag.Buscar = buscar;
        ViewBag.Estado = estado;

        return View(lista);
    }

    // GET: Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CategoriaViewModel categoria)
    {
        if (!ModelState.IsValid)
            return View(categoria);

        await _service.Crear(categoria);

        return RedirectToAction(nameof(Index));
    }

    // GET: Edit
    public async Task<IActionResult> Edit(int id)
    {
        var categoria =
            await _service.ObtenerPorId(id);

        if (categoria == null)
            return NotFound();

        return View(categoria);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        CategoriaViewModel categoria)
    {
        if (id != categoria.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(categoria);

        await _service.Actualizar(id, categoria);

        return RedirectToAction(nameof(Index));
    }

    // GET: Details
    public async Task<IActionResult> Details(int id)
    {
        var categoria =
            await _service.ObtenerPorId(id);

        if (categoria == null)
            return NotFound();

        return View(categoria);
    }

    // GET: Delete
    public async Task<IActionResult> Delete(int id)
    {
        var categoria =
            await _service.ObtenerPorId(id);

        if (categoria == null)
            return NotFound();

        return View(categoria);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.Eliminar(id);

        return RedirectToAction(nameof(Index));
    }
}