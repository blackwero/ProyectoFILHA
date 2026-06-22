using ProyectoFILHAAPI.Entidades.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFILHAPI.Entidades
{
    [Table("DOMICILIO")]
    public class Domicilio
    {
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(200)]
        [Column("CALLE")]
        public string? Calle { get; set; }

        [StringLength(200)]
        [Column("COLONIA")]
        public string? Colonia { get; set; }

        [StringLength(100)]
        [Column("MUNICIPIO")]
        public string? Municipio { get; set; }

        [StringLength(100)]
        [Column("ESTADO_LUGAR")]
        public string? EstadoLugar { get; set; }

        [StringLength(100)]
        [Column("PAIS")]
        public string? Pais { get; set; }

        [StringLength(300)]
        [Column("ENTRECALLES")]
        public string? EntreCalles { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }

        [Column("ESTADO")]
        public EstadoEnum? Estado { get; set; }

        [StringLength(10)]
        [Column("CP")]
        public string? CodigoPostal { get; set; }

        [Column("CLIENTE")]
        public int? ClienteId { get; set; }

        // Relación
        public Cliente? Cliente { get; set; }
    }
}