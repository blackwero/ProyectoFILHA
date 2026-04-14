using ProyectoFILHA.Models.DTOs;

namespace ProyectoFILHA.Services.Interfaces
{
    public interface IDragonBallService
    {
        Task<DragonBallResponse> ObtenerPersonajes();
    }
}
