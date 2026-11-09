using System.ComponentModel.DataAnnotations;

namespace cine.Models
{
    // Tabla: Peliculas
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public int DuracionMinutos { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public List<Funcion> Funciones { get; set; } = new();
    }

    // Tabla: Salas
    public class Sala
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty; //Sala 1 - 3D
        public int TotalFilas { get; set; } //A hasta F
        public int AsientosPorFila { get; set; } //1 al 8
        public List<Funcion> Funciones { get; set; } = new();
    }

    // Tabla: Funciones (Horarios y precios)
    public class Funcion
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula? Pelicula { get; set; }
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal PrecioEntrada { get; set; }
        public List<Reserva> Reservas { get; set; } = new();
    }

    // Tabla: Reservas (Encabezado de la compra)
    public class Reserva
    {
        public int Id { get; set; }
        public int FuncionId { get; set; }
        public Funcion? Funcion { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
        public decimal TotalPagado { get; set; }
        public List<ReservaDetalle> Detalles { get; set; } = new();
    }

    // Tabla: ReservaDetalle (Asientos específicos comprados)
    public class ReservaDetalle
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public Reserva? Reserva { get; set; }
        public char Fila { get; set; } 
        public int NumeroAsiento { get; set; } 
    }
}