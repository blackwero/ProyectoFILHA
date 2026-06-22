using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFILHAMAUI.Models
{
    public class UsuarioSesion
    {
        public int UsuarioId { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int? ClienteId { get; set; }
        public string? NombreCliente { get; set; }
    }
}
