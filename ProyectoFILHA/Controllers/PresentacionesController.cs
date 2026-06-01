using Microsoft.AspNetCore.Mvc;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;
using ProyectoFILHA.Services;

public class PresentacionesController : Controller
{
    private readonly PresentacionService _service;

    public PresentacionesController(
        PresentacionService service)
    {
        _service = service;
    }

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
                .Where(x => x.Nombre != null &&
                       x.Nombre.Contains(
                           buscar,
                           StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (estado.HasValue)
        {
            lista = lista
                .Where(x => x.Estado == estado.Value)
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

        return View(lista);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PresentacionViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.Crear(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model =
            await _service.ObtenerPorId(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        int id,
        PresentacionViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.Actualizar(id, model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var model =
            await _service.ObtenerPorId(id);

        return View(model);
    }
}