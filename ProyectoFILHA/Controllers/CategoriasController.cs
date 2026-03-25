using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;

public class CategoriasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTADO + FILTROS
    public async Task<IActionResult> Index(string buscar, EstadoEnum? estado, int page = 1)
    {
        int pageSize = 10;

        var query = _context.Categorias.AsQueryable();

        if (!string.IsNullOrEmpty(buscar))
        {
            query = query.Where(c => c.Nombre.Contains(buscar));
        }

        if (estado.HasValue)
        {
            query = query.Where(c => c.Estado == estado);
        }

        int totalItems = await query.CountAsync();

        var lista = await query
            .OrderBy(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

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
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            categoria.FechaCreacion = DateTime.Now;
            _context.Add(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

    // GET: Edit
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        return View(categoria);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categoria categoria)
    {
        if (id != categoria.Id) return NotFound();

        var categoriaDb = await _context.Categorias.FindAsync(id);
        if (categoriaDb == null) return NotFound();

        if (ModelState.IsValid)
        {
            // Solo actualizamos lo necesario
            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Estado = categoria.Estado;

            // NO tocamos FechaCreacion

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(categoria);
    }

    // GET: Details
    public async Task<IActionResult> Details(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        return View(categoria);
    }


}