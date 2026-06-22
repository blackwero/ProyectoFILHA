using System.Net.Http.Json;
using ProyectoFILHAMAUI.Models;

namespace ProyectoFILHAMAUI.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool exito, UsuarioSesion? sesion, string? error)> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var sesion = await response.Content.ReadFromJsonAsync<UsuarioSesion>();
                return (true, sesion, null);
            }

            var mensajeError = await response.Content.ReadAsStringAsync();
            return (false, null, string.IsNullOrWhiteSpace(mensajeError) ? "No se pudo iniciar sesión." : mensajeError);
        }

        public async Task<(bool exito, UsuarioSesion? sesion, string? error)> RegistroAsync(RegistroRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/registro", request);

            if (response.IsSuccessStatusCode)
            {
                var sesion = await response.Content.ReadFromJsonAsync<UsuarioSesion>();
                return (true, sesion, null);
            }

            var mensajeError = await response.Content.ReadAsStringAsync();
            return (false, null, string.IsNullOrWhiteSpace(mensajeError) ? "No se pudo crear la cuenta." : mensajeError);
        }
    }
}