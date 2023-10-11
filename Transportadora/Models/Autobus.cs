namespace Transportadora.Models
{
    public class Autobus
    {

        public int Id { get; set; }
        public string Placa { get; set; }
        public bool EnOperacion { get; set; }
        public DateTime TiempoUltimoMantenimiento { get; set; }
        public int HorasEnOperacion { get; set; }
        public int CargadorId { get; set; }
        public Cargador Cargador { get; set; }
        public bool EnUso { get; set; }
        public List<UsoAutobus> Usos { get; set; }

    }
}
