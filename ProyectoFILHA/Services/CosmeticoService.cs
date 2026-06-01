using ProyectoFILHA.Models.Entidades;
using System.Net.Http.Json;

namespace ProyectoFILHA.Services
{
    public class CosmeticoService
    {
        private readonly HttpClient _httpClient;

        public CosmeticoService(
            IHttpClientFactory factory)
        {
            _httpClient =
                factory.CreateClient("API");
        }

        public async Task<List<CosmeticoViewModel>> ObtenerTodos()
        {
            return await _httpClient
                .GetFromJsonAsync<List<CosmeticoViewModel>>(
                    "api/cosmeticos");
        }

        public async Task<CosmeticoViewModel> ObtenerPorId(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<CosmeticoViewModel>(
                    $"api/cosmeticos/{id}");
        }

        public async Task Crear(CosmeticoViewModel cosmetico)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/cosmeticos",
                    cosmetico);

            response.EnsureSuccessStatusCode();
        }

        public async Task Actualizar(
            int id,
            CosmeticoViewModel cosmetico)
        {
            var response =
                await _httpClient.PutAsJsonAsync(
                    $"api/cosmeticos/{id}",
                    cosmetico);

            response.EnsureSuccessStatusCode();
        }

        public async Task Eliminar(int id)
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"api/cosmeticos/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}