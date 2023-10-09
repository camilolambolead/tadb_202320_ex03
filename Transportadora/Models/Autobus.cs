namespace Transportadora.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        public required string Placa { get; set; }
        public bool EnOperacion { get; set; }
        public DateTime TiempoUltimoMantenimiento { get; set; } // Hora del último mantenimiento
        public int HorasEnOperacion { get; set; } // Total de horas en operación

        // Agregar una propiedad para la clave foránea del Cargador
        public int CargadorId { get; set; }
        public Cargador Cargador { get; set; }

    }
}
