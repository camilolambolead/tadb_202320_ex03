namespace Transportadora.Models
{
    public class UsoAutobus
    {
        public int Id { get; set; }

        // Propiedades relacionadas con el uso del autobús
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }

        // Clave foránea para relacionar con el autobús
        public int AutobusId { get; set; }
        public Autobus Autobus { get; set; }
    }
}
