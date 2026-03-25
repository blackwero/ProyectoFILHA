namespace ProyectoFILHA.Models.Entidades
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Pedido
    {
        public int Id { get; set; }
        [Column("FECHA_PEDIDO")]
        public DateTime? FechaPedido { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        public decimal? TotalPago { get; set; }

        public int? Estado { get; set; }

        public string? FecEstimEntrega { get; set; }

        public int MedioPagoId { get; set; }
        public int ClienteId { get; set; }

        [ForeignKey("MedioPagoId")]
        public MedioPago? MedioPago { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        public List<CarritoPedidoCosmetico>? Detalle { get; set; }
    }
}
