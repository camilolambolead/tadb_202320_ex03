using Transportadora.Models;

namespace Transportadora.Interfaces
{
    public interface ICargadorRepository
    {
        bool IsCargadorInUseDuringPeakHours(Cargador cargador);
        List<Cargador> GetAllCargadores();
        Cargador GetCargadorById(int cargadorId);
        void CreateCargador(Cargador cargador);
        void UpdateCargador(Cargador cargador);
        void DeleteCargador(int cargadorId);
    }
}
