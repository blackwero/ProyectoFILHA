using ProyectoFILHA.Models.DTOs;

namespace ProyectoFILHA.Services.Interfaces
{
    public interface ITriviaService
    {
        Task<TriviaResponse> ObtenerPreguntas();
    }
}
