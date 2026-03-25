using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFILHA.Models.Entidades
{
    public class CarritoCompras
    {
        public int Id { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FecCreacion { get; set; }
        public int? Estado { get; set; }

        public List<CarritoPedidoCosmetico>? Items { get; set; }
    }
}
