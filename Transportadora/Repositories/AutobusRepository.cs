namespace Transportadora.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Transportadora.Data;
    using Transportadora.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AutobusRepository
    {
        private readonly TransporteContext _context;

        public AutobusRepository(TransporteContext context)
        {
            _context = context;
        }

        public List<Autobus> GetAllAutobuses()
        {
            return _context.Autobuses.ToList();
        }
        public Autobus GetAutobusById(int autobusId)
        {
            return _context.Autobuses.FirstOrDefault(a => a.Id == autobusId);
        }
        public void CreateAutobus(Autobus autobus)
        {
            _context.Autobuses.Add(autobus);
            _context.SaveChanges();
        }
        public void UpdateAutobus(Autobus autobus)
        {
            _context.Autobuses.Update(autobus);
            _context.SaveChanges();
        }
        public void DeleteAutobus(int autobusId)
        {
            var autobus = _context.Autobuses.FirstOrDefault(a => a.Id == autobusId);
            if (autobus != null)
            {
                _context.Autobuses.Remove(autobus);
                _context.SaveChanges();
            }
        }

    }

}
