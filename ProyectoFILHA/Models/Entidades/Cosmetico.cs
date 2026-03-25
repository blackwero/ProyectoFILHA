namespace ProyectoFILHA.Models.Entidades
{
    using ProyectoFILHA.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cosmetico
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        public decimal? Precio { get; set; }

        public int? CantDisponible { get; set; }
        public EstadoEnum Estado { get; set; }

        [Column("CATEGORIA")]
        public int CategoriaId { get; set; }

        [Column("PRESENTACION")]
        public int PresentacionId { get; set; }

        public string? Descripcion { get; set; }

        public int? EsVegano { get; set; }
        public int? EsDermatologico { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }

        // 🔥 Relaciones (opcionales)
        public Categoria? Categoria { get; set; }
        public Presentacion? Presentacion { get; set; }

        public List<CarritoPedidoCosmetico>? CarritoItems { get; set; }
    }
}
