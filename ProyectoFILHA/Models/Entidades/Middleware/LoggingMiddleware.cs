using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using System.Diagnostics;
using System.Security.Claims;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ApplicationDbContext db)
    {
        var stopwatch = Stopwatch.StartNew();

        var usuario = context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonimo";

        try
        {
            await _next(context);

            stopwatch.Stop();

            var log = new Log
            {
                Nivel = "INFO",
                Mensaje = $"{context.Request.Method} {context.Request.Path} - {context.Response.StatusCode} en {stopwatch.ElapsedMilliseconds}ms",
                Fecha = DateTime.Now,
                Usuario = usuario
            };

            db.Logs.Add(log);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            var log = new Log
            {
                Nivel = "ERROR",
                Mensaje = ex.Message,
                Fecha = DateTime.Now,
                Usuario = usuario
            };

            db.Logs.Add(log);
            await db.SaveChangesAsync();

            throw;
        }
    }
}