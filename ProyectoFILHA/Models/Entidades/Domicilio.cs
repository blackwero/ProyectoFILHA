namespace ProyectoFILHA.Models.Entidades
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Domicilio
    {
        public int Id { get; set; }

        public string? Calle { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? EstadoLugar { get; set; }
        public string? Pais { get; set; }
        public string? EntreCalles { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public int? Estado { get; set; }

        public string? CP { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
    }
}
