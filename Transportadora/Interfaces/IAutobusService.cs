using Transportadora.Models;

namespace Transportadora.Interfaces
{
    public interface IAutobusService
    {
        List<Autobus> GetAllAutobuses();
        Autobus GetAutobusById(int autobusId);
        void CreateAutobus(Autobus autobus);
        void UpdateAutobus(Autobus autobus);
        void DeleteAutobus(int autobusId);
    }
}
