using ProyectoFILHAAPI.Entidades.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFILHAPI.Entidades
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("CORREO")]
        public string Correo { get; set; }

        [Required]
        [StringLength(300)]
        [Column("PASSWORD_HASH")]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ROL")]
        public string Rol { get; set; } = "Cliente";

        [Column("CLIENTE")]
        public int? ClienteId { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }

        [Column("ESTADO")]
        public EstadoEnum Estado { get; set; }

        // Relación
        public Cliente? Cliente { get; set; }
    }
}