namespace Transportadora.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Transportadora.Data;
    using Transportadora.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CargadorRepository
    {
        private readonly TransporteContext _context;

        public CargadorRepository(TransporteContext context)
        {
            _context = context;
        }

        // Métodos CRUD para la entidad Cargador

        public List<Cargador> GetAllCargadores()
        {
            return _context.Cargadores.ToList();
        }

        public Cargador GetCargadorById(int cargadorId)
        {
            return _context.Cargadores.FirstOrDefault(c => c.Id == cargadorId);
        }
        public void CreateCargador(Cargador cargador)
        {
            _context.Cargadores.Add(cargador);
            _context.SaveChanges();
        }
        public void UpdateCargador(Cargador cargador)
        {
            _context.Cargadores.Update(cargador);
            _context.SaveChanges();
        }
        public void DeleteCargador(int cargadorId)
        {
            var cargador = _context.Cargadores.FirstOrDefault(c => c.Id == cargadorId);
            if (cargador != null)
            {
                _context.Cargadores.Remove(cargador);
                _context.SaveChanges();
            }
        }


    }

}
