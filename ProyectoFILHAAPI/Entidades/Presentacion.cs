using ProyectoFILHAAPI.Entidades.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFILHAAPI.Entidades
{
    [Table("PRESENTACION")]
    public class Presentacion
    {
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(100)]
        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }

        [Column("ESTADO")]
        public EstadoEnum Estado { get; set; }

        // Relación
       // public List<Cosmetico>? Cosmeticos { get; set; }
    }
}
