namespace ProyectoFILHA.Models.Entidades
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MedioPago
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime? FecCreacion { get; set; }
        public int? Estado { get; set; }

        public List<Pedido>? Pedidos { get; set; }
    }
}
