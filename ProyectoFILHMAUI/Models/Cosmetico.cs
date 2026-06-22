using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFILHMAUI.Models
{
    public class Cosmetico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? CantDisponible { get; set; }
        public int Estado { get; set; }
        public int CategoriaId { get; set; }
        public int PresentacionId { get; set; }
        public string? Descripcion { get; set; }
        public int? EsVegano { get; set; }
        public int? EsDermatologico { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public Categoria? Categoria { get; set; }
        public Presentacion? Presentacion { get; set; }
    }



}
