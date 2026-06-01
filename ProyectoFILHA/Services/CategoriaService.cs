namespace ProyectoFILHA.Services
{
    using ProyectoFILHA.Models;
    using ProyectoFILHA.Models.Entidades;
    using System.Net.Http.Json;

  
        public class CategoriaService
        {
            private readonly HttpClient _httpClient;

            public CategoriaService(IHttpClientFactory factory)
            {
                _httpClient = factory.CreateClient("API");
            }

            // GET
            public async Task<List<CategoriaViewModel>> ObtenerTodos()
            {
                return await _httpClient
                    .GetFromJsonAsync<List<CategoriaViewModel>>(
                        "api/categorias");
            }

            // GET ID
            public async Task<CategoriaViewModel> ObtenerPorId(int id)
            {
                return await _httpClient
                    .GetFromJsonAsync<CategoriaViewModel>(
                        $"api/categorias/{id}");
            }

            // POST
            public async Task Crear(CategoriaViewModel categoria)
            {
                await _httpClient.PostAsJsonAsync(
                    "api/categorias",
                    categoria);
            }

            // PUT
            public async Task Actualizar(
                int id,
                CategoriaViewModel categoria)
            {
                await _httpClient.PutAsJsonAsync(
                    $"api/categorias/{id}",
                    categoria);
            }

            // DELETE
            public async Task Eliminar(int id)
            {
                await _httpClient.DeleteAsync(
                    $"api/categorias/{id}");
            }

 
    }
    
}
