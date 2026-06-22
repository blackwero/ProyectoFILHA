using ProyectoFILHAAPI.Entidades.Enums;


namespace ProyectoFILHAPI.DTOs
{
    public class RegistroRequestDto
    {
        // Datos de acceso
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Datos de perfil (Cliente)
        public string Nombre { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string? Telefono { get; set; }
    }
}