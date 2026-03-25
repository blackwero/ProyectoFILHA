namespace ProyectoFILHA.Models.Entidades
{
    using ProyectoFILHA.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Presentacion
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }
        public EstadoEnum Estado { get; set; }

        // Relación
        public List<Cosmetico>? Cosmeticos { get; set; }
    }
}
