namespace Transportadora.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Transportadora.Data;
    using Transportadora.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class HorarioRepository
    {
        private readonly TransporteContext _context;

        public HorarioRepository(TransporteContext context)
        {
            _context = context;
        }

        public List<Horario> GetAllHorarios()
        {
            return _context.Horarios.ToList();
        }
        public Horario GetHorarioById(int horarioId)
        {
            return _context.Horarios.FirstOrDefault(h => h.Id == horarioId);
        }
        public void CreateHorario(Horario horario)
        {
            _context.Horarios.Add(horario);
            _context.SaveChanges();
        }
        public void UpdateHorario(Horario horario)
        {
            _context.Horarios.Update(horario);
            _context.SaveChanges();
        }
        public void DeleteHorario(int horarioId)
        {
            var horario = _context.Horarios.FirstOrDefault(h => h.Id == horarioId);
            if (horario != null)
            {
                _context.Horarios.Remove(horario);
                _context.SaveChanges();
            }
        }

    }
}
