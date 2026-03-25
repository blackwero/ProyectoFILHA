using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;

public class PresentacionesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PresentacionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 🔎 LISTADO + FILTROS + PAGINACIÓN
    public async Task<IActionResult> Index(string buscar, EstadoEnum? estado, int page = 1)
    {
        int pageSize = 10;

        var query = _context.Presentaciones.AsQueryable();

        if (!string.IsNullOrEmpty(buscar))
        {
            query = query.Where(p => p.Nombre.Contains(buscar));
        }

        if (estado.HasValue)
        {
            query = query.Where(p => p.Estado == estado);
        }

        int totalItems = await query.CountAsync();

        var lista = await query
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        ViewBag.Buscar = buscar;
        ViewBag.Estado = estado;

        return View(lista);
    }

    // 🟢 CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Presentacion presentacion)
    {
        if (ModelState.IsValid)
        {
            presentacion.FechaCreacion = DateTime.Now;
            _context.Add(presentacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(presentacion);
    }

    // ✏️ EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var presentacion = await _context.Presentaciones.FindAsync(id);
        if (presentacion == null) return NotFound();

        return View(presentacion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Presentacion presentacion)
    {
        if (id != presentacion.Id) return NotFound();

        var presentacionDb = await _context.Presentaciones.FindAsync(id);
        if (presentacionDb == null) return NotFound();

        if (ModelState.IsValid)
        {
            presentacionDb.Nombre = presentacion.Nombre;
            presentacionDb.Estado = presentacion.Estado;
            // 🔒 NO tocar FechaCreacion

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(presentacion);
    }

    // 🔍 DETAILS
    public async Task<IActionResult> Details(int id)
    {
        var presentacion = await _context.Presentaciones.FindAsync(id);
        if (presentacion == null) return NotFound();

        return View(presentacion);
    }
}