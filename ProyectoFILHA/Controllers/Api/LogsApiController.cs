using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LogsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LogsApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/logs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
    {
        return await _context.Logs
            .OrderByDescending(l => l.Fecha)
            .ToListAsync();
    }

    // GET: api/logs/filtrar
    [HttpGet("filtrar")]
    public async Task<ActionResult<IEnumerable<Log>>> Filtrar(string? usuario, string? nivel)
    {
        var query = _context.Logs.AsQueryable();

        if (!string.IsNullOrEmpty(usuario))
            query = query.Where(l => l.Usuario.Contains(usuario));

        if (!string.IsNullOrEmpty(nivel))
            query = query.Where(l => l.Nivel == nivel);

        return await query
            .OrderByDescending(l => l.Fecha)
            .ToListAsync();
    }
}