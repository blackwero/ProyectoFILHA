using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;

public class ClientesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientesController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CrearCuenta()
    {
        return View();
    }

    public IActionResult Entrar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearCuenta(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            // 🔥 Valores automáticos
            cliente.FechaCreacion = DateTime.Now;
            cliente.Estado = (ProyectoFILHA.Models.Enums.EstadoEnum)1; // Activo

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // o a tienda después
        }

        return View(cliente);
    }
}
