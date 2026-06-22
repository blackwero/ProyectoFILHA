namespace ProyectoFILHAPI.DTOs
{
    public class LoginResponseDto
    {
        public int UsuarioId { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int? ClienteId { get; set; }
        public string? NombreCliente { get; set; }
    }
}