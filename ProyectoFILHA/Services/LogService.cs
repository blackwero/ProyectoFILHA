using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;

public class LogService : ILogService
{
    private readonly ApplicationDbContext _context;

    public LogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task GuardarLog(string nivel, string mensaje, string? usuario = null)
    {
        var log = new Log
        {
            Nivel = nivel,
            Mensaje = mensaje,
            Fecha = DateTime.Now,
            Usuario = usuario
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }
}