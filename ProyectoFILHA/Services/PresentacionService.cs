using ProyectoFILHA.Models.Entidades;

namespace ProyectoFILHA.Services
{
    public class PresentacionService
    {
        private readonly HttpClient _httpClient;

        public PresentacionService(
            IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        // GET
        public async Task<List<PresentacionViewModel>>
            ObtenerTodos()
        {
            return await _httpClient
                .GetFromJsonAsync<
                    List<PresentacionViewModel>>(
                        "api/presentaciones");
        }

        // GET ID
        public async Task<PresentacionViewModel>
            ObtenerPorId(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<
                    PresentacionViewModel>(
                        $"api/presentaciones/{id}");
        }

        // POST
        public async Task Crear(
            PresentacionViewModel model)
        {
            await _httpClient.PostAsJsonAsync(
                "api/presentaciones",
                model);
        }

        // PUT
        public async Task Actualizar(
            int id,
            PresentacionViewModel model)
        {
            await _httpClient.PutAsJsonAsync(
                $"api/presentaciones/{id}",
                model);
        }

        // DELETE
        public async Task Eliminar(int id)
        {
            await _httpClient.DeleteAsync(
                $"api/presentaciones/{id}");
        }
    }
}
