using System.Text.Json;
using ProyectoFILHAMAUI.Models;

namespace ProyectoFILHAMAUI.Services
{
    public static class SessionService
    {
        private const string Key = "usuario_sesion";

        public static async Task GuardarSesionAsync(UsuarioSesion sesion)
        {
            var json = JsonSerializer.Serialize(sesion);
            await SecureStorage.SetAsync(Key, json);
        }

        public static async Task<UsuarioSesion?> ObtenerSesionAsync()
        {
            try
            {
                var json = await SecureStorage.GetAsync(Key);
                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return JsonSerializer.Deserialize<UsuarioSesion>(json);
            }
            catch
            {
                return null;
            }
        }

        public static void CerrarSesion()
        {
            SecureStorage.Remove(Key);
        }

        public static async Task<bool> HaySesionActivaAsync()
        {
            var sesion = await ObtenerSesionAsync();
            return sesion != null;
        }
    }
}