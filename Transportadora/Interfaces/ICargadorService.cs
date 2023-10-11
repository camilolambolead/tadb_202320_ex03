using Transportadora.Models;

namespace Transportadora.Interfaces
{
    public interface ICargadorService
    {
        List<Cargador> GetAllCargadores();
        Cargador GetCargadorById(int id);
        void CreateCargador(Cargador cargador);
        void UpdateCargador(Cargador cargador);
        void DeleteCargador(int id);
        bool IsCargadorInUseDuringPeakHours(Cargador cargador);
    }
}
