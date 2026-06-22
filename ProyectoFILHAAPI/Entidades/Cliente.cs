using ProyectoFILHAAPI.Entidades.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFILHAPI.Entidades
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("FEC_NAC")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("GENERO")]
        public GeneroEnum? Genero { get; set; }

        [StringLength(100)]
        [Column("CORREO")]
        public string? Correo { get; set; }

        [StringLength(50)]
        [Column("TELEFONO")]
        public string? Telefono { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }

        [Column("ESTADO")]
        public EstadoEnum? Estado { get; set; }

        // Relaciones
        public List<Domicilio>? Domicilios { get; set; }
        public Usuario? Usuario { get; set; }
    }
}