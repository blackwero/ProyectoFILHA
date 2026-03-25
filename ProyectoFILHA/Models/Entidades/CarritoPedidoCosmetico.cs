namespace ProyectoFILHA.Models.Entidades
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class CarritoPedidoCosmetico
    {
        public int CarritoComprasId { get; set; }
        public int PedidoId { get; set; }
        public int CosmeticoId { get; set; }
        public int Cantidad {  get; set; }

        [ForeignKey("CarritoComprasId")]
        public CarritoCompras? CarritoCompras { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido? Pedido { get; set; }

        [ForeignKey("CosmeticoId")]
        public Cosmetico? Cosmetico { get; set; }
    }
}
