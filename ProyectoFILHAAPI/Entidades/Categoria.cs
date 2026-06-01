namespace ProyectoFILHAAPI.Entidades
{
    using ProyectoFILHAAPI.Entidades.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ProyectoFILHA.API.Models.Entidades
    {
        [Table("CATEGORIA")]
        public class Categoria
        {
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string? Nombre { get; set; } = string.Empty;

            [Column("FECHA_CREACION")]
            public DateTime? FechaCreacion { get; set; }

            public EstadoEnum Estado { get; set; }

            // Relación
            //public List<Cosmetico>? Cosmeticos { get; set; }
        }
    }
}
