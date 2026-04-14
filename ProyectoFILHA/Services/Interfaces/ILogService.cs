using ProyectoFILHA.Models.Entidades;

public interface ILogService
{
    Task GuardarLog(string nivel, string mensaje, string? usuario = null);
}