using Microsoft.EntityFrameworkCore;
using Transportadora.Data;
using Transportadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Transportadora.Interfaces;

namespace Transportadora.Repositories
{
    public class CargadorRepository : ICargadorRepository
    {
        private readonly TransporteContext _context;

        public CargadorRepository(TransporteContext context)
        {
            _context = context;
        }

        public bool IsCargadorInUseDuringPeakHours(Cargador cargador)
        {
            DateTime horaActual = DateTime.Now;

            // Verifica si la hora actual está dentro del horario pico
            bool esHorarioPico = (horaActual.Hour >= 5 && horaActual.Hour < 9) || (horaActual.Hour >= 16 && horaActual.Hour < 20);

            if (esHorarioPico)
            {
                // Verifica si el cargador está en uso durante el horario pico
                if (cargador.EnUso)
                {
                    return true; // El cargador está en uso durante el horario pico
                }
            }

            // Si no es horario pico o el cargador no está en uso, retorna false
            return false;
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
