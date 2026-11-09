using Microsoft.EntityFrameworkCore;

namespace cine.Models
{
    public class CineDbContext : DbContext
    {
        public CineDbContext(DbContextOptions<CineDbContext> options) : base(options) { }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Funcion> Funciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaDetalle> DetallesReserva { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de columnas de tipo moneda para PostgreSQL
            modelBuilder.Entity<Funcion>().Property(f => f.PrecioEntrada).HasColumnType("numeric(18,2)");
            modelBuilder.Entity<Reserva>().Property(r => r.TotalPagado).HasColumnType("numeric(18,2)");
        }
    }
}