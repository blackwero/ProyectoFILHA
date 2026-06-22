using Microsoft.EntityFrameworkCore;
using ProyectoFILHAAPI.Entidades;
using ProyectoFILHAAPI.Entidades.ProyectoFILHA.API.Models.Entidades;
using ProyectoFILHAPI.Entidades;

namespace ProyectoFILHA.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Presentacion> Presentaciones { get; set; }
        public DbSet<Cosmetico> Cosmeticos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // public DbSet<Cosmetico> Cosmeticos { get; set; }
    }
}