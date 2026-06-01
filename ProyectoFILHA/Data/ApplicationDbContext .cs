using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models.Entidades;

namespace ProyectoFILHA.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<CategoriaViewModel> Categorias { get; set; }
        public DbSet<PresentacionViewModel> Presentaciones { get; set; }
        public DbSet<CosmeticoViewModel> Cosmeticos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<MedioPago> MediosPago { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<CarritoCompras> CarritosCompras { get; set; }
        public DbSet<CarritoPedidoCosmetico> CarritoPedidoCosmeticos { get; set; }
        public DbSet<UsuarioApi> UsuariosApi { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

     
            modelBuilder.Entity<CarritoPedidoCosmetico>()
                .HasKey(c => new { c.CarritoComprasId, c.PedidoId, c.CosmeticoId });

   
            modelBuilder.Entity<CarritoPedidoCosmetico>()
                .HasOne(c => c.CarritoCompras)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CarritoComprasId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CarritoPedidoCosmetico>()
                .HasOne(c => c.Pedido)
                .WithMany(p => p.Detalle)
                .HasForeignKey(c => c.PedidoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CarritoPedidoCosmetico>()
                .HasOne(c => c.Cosmetico)
                .WithMany(c => c.CarritoItems)
                .HasForeignKey(c => c.CosmeticoId)
                .OnDelete(DeleteBehavior.NoAction);

           
            modelBuilder.Entity<CosmeticoViewModel>()
                .Property(c => c.Precio)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.TotalPago)
                .HasPrecision(14, 2);

           
            modelBuilder.Entity<CategoriaViewModel>().ToTable("CATEGORIA");
            modelBuilder.Entity<PresentacionViewModel>().ToTable("PRESENTACION");
            modelBuilder.Entity<CosmeticoViewModel>().ToTable("COSMETICO");
            modelBuilder.Entity<Cliente>().ToTable("CLIENTE");
            modelBuilder.Entity<Domicilio>().ToTable("DOMICILIO");
            modelBuilder.Entity<MedioPago>().ToTable("MEDIO_PAGO");
            modelBuilder.Entity<Pedido>().ToTable("PEDIDO");
            modelBuilder.Entity<CarritoCompras>().ToTable("CARRITOCOMPRAS");
            modelBuilder.Entity<CarritoPedidoCosmetico>().ToTable("CARRITOPEDIDOCOSMETICO");
        }
    }
}