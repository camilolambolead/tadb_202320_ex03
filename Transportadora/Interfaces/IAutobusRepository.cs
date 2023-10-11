using Transportadora.Models;

namespace Transportadora.Interfaces
{
    public interface IAutobusRepository
    {
        List<Autobus> GetAllAutobuses();
        Autobus GetAutobusById(int id);
        void CreateAutobus(Autobus autobus);
        void UpdateAutobus(Autobus autobus);
        void DeleteAutobus(int id);

        public bool IsAutobusInUseDuringPeakHours(Autobus autobus);
        void RegisterAutobusUsage(Autobus autobus, DateTime usageTime);
        void UpdateAutobusUsage(Autobus autobus, DateTime usageTime);
        void DeleteAutobusUsage(Autobus autobus, DateTime usageTime);
        
        List<UsoAutobus> GetAutobusUsage(int autobusId);
    }
}
