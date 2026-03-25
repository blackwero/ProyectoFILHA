namespace ProyectoFILHA.Models.Entidades
{
    using ProyectoFILHA.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string? Nombre { get; set; }

        [Column("FEC_NAC")]
        public DateTime? FecNac { get; set; }
        public int? Genero { get; set; }

        [EmailAddress]
        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime? FechaCreacion { get; set; }
        public EstadoEnum Estado { get; set; }

        // Relaciones
        public List<Domicilio>? Domicilios { get; set; }
        public List<Pedido>? Pedidos { get; set; }
    }
}
