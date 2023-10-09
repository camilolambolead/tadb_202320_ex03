namespace Transportadora.Models
{
    public class Cargador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public bool EnUso { get; set; }

        // Agregar una propiedad de navegación para Autobuses
        public List<Autobus> Autobuses { get; set; }
    }
}