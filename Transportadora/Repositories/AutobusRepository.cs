using Microsoft.EntityFrameworkCore;
using Transportadora.Data;
using Transportadora.Models;
using System.Collections.Generic;
using System.Linq;
using Transportadora.Interfaces;

namespace Transportadora.Repositories
{
    public class AutobusRepository : IAutobusRepository
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
        private bool IsAutobusInUseDuringPeakHours(Autobus autobus)
        {
            DateTime horaActual = DateTime.Now;

            bool esHorarioPico = (horaActual.Hour >= 5 && horaActual.Hour < 9) || (horaActual.Hour >= 16 && horaActual.Hour < 20);

            if (esHorarioPico)
            {
                // Verifica si el autobús está en uso durante el horario pico
                if (autobus.EnUso)
                {
                    return true; // El autobús está en uso durante el horario pico
                }
            }

            // Si no es horario pico o el autobús no está en uso, retorna false
            return false;
        }


        public void RegisterAutobusUsage(Autobus autobus, DateTime usageTime)
        {
            // Obtenemos el autobús por su ID
            var autobusId = GetAutobusById(autobus.Id);
            if (autobus == null)
            {
                // El autobús no existe
                throw new Exception("El autobús no existe.");
            }

            // Verificamos si el autobús está en uso durante el horario pico
            if (IsAutobusInUseDuringPeakHours(autobus))
            {
                throw new Exception("No se puede registrar la utilización de un autobús en horario pico.");
            }

            // Registramos la utilización del autobús
            autobus.Usos.Add(new UsoAutobus { HoraInicio = usageTime, HoraFin = usageTime.AddHours(6) });

            // Guardamos los cambios en la base de datos
            _context.SaveChanges();
        }

        public void UpdateAutobusUsage(Autobus autobus, DateTime usageTime)
        {
            // Obtenemos el autobús por su ID
            var autobusId = GetAutobusById(autobus);
            if (autobusId == null)
            {
                // El autobús no existe
                throw new Exception("El autobús no existe.");
            }

            // Verificamos si el autobús está en uso durante el horario pico
            if (IsAutobusInUseDuringPeakHours(autobusId))
            {
                throw new Exception("No se puede actualizar la utilización de un autobús en horario pico.");
            }

            // Actualizamos la utilización del autobús
            var usageToUpdate = autobus.Usos.FirstOrDefault(uso => uso.HoraInicio <= usageTime && uso.HoraFin >= usageTime);
            if (usageToUpdate != null)
            {
                usageToUpdate.HoraInicio = usageTime;
                usageToUpdate.HoraFin = usageTime.AddHours(6);

                // Guardamos los cambios en la base de datos
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("La utilización del autobús no existe en el horario especificado.");
            }
        }

        public void DeleteAutobusUsage(Autobus autobus, DateTime usageTime)
        {
            // Obtenemos el autobús por su ID
            var autobusid = GetAutobusById(autobus.Id);
            if (autobus == null)
            {
                // El autobús no existe
                throw new Exception("El autobús no existe.");
            }

            // Verificamos si el autobús está en uso durante el horario pico
            if (IsAutobusInUseDuringPeakHours(autobus))
            {
                throw new Exception("No se puede eliminar la utilización de un autobús en horario pico.");
            }

            // Eliminamos la utilización del autobús
            var usageToRemove = autobus.Usos.FirstOrDefault(uso => uso.HoraInicio <= usageTime && uso.HoraFin >= usageTime);
            if (usageToRemove != null)
            {
                autobus.Usos.Remove(usageToRemove);

                // Guardamos los cambios en la base de datos
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("La utilización del autobús no existe en el horario especificado.");
            }
        }

       

        public List<UsoAutobus> GetAutobusUsage(int autobusId)
        {
            // Obtenemos los usos registrados para un autobús específico
            return _context.UsosAutobus.Where(uso => uso.AutobusId == autobusId).ToList();
        }

    }
}
