using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("LOGS")]
public class Log
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NIVEL")]
    public string Nivel { get; set; }

    [Column("MENSAJE")]
    public string Mensaje { get; set; }

    [Column("FECHA")]
    public DateTime Fecha { get; set; }

    [Column("USUARIO")]
    public string? Usuario { get; set; }
}