namespace Transportadora.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DateTime Hora { get; set; }
        public bool EsHorarioPico { get; set; }
        public int BusesEnOperacion { get; set; } // Número de autobuses en operación en este horario
        public int CargadoresEnUso { get; set; } // Número de cargadores en uso en este horario
    }
}
