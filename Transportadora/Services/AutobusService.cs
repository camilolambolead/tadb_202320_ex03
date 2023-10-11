using System;
using System.Collections.Generic;
using System.Linq;
using Transportadora.Interfaces;
using Transportadora.Models;
using Transportadora.Repositories;

namespace Transportadora.Services
{
    public class AutobusService : IAutobusService
    {
        private readonly IAutobusRepository _autobusRepository;

        public AutobusService(IAutobusRepository autobusRepository)
        {
            _autobusRepository = autobusRepository;
        }

        public List<Autobus> GetAllAutobuses()
        {
            return _autobusRepository.GetAllAutobuses();
        }

        public Autobus GetAutobusById(int autobusId)
        {
            return _autobusRepository.GetAutobusById(autobusId);
        }

        public void CreateAutobus(Autobus autobus)
        {
            // Realiza validaciones antes de crear el autobús
            if (autobus != null)
            {
                if (IsAutobusInUseDuringPeakHours(autobus))
                {
                    // No se permite crear un autobús en horario pico
                    throw new Exception("No se puede crear un autobús en horario pico.");
                }

                _autobusRepository.CreateAutobus(autobus);
            }
        }

        public void UpdateAutobus(Autobus autobus)
        {
            if (autobus != null)
            {
                if (IsAutobusInUseDuringPeakHours(autobus))
                {
                    // No se permite actualizar un autobús en horario pico
                    throw new Exception("No se puede actualizar un autobús en horario pico.");
                }

                _autobusRepository.UpdateAutobus(autobus);
            }
        }

        public void DeleteAutobus(int autobusId)
        {
            var autobus = GetAutobusById(autobusId);
            if (autobus != null)
            {
                if (IsAutobusInUseDuringPeakHours(autobus))
                {
                    // No se permite eliminar un autobús en horario pico
                    throw new Exception("No se puede eliminar un autobús en horario pico.");
                }

                _autobusRepository.DeleteAutobus(autobusId);
            }
        }

        // Agregar más métodos y lógica según tus necesidades

        private bool IsAutobusInUseDuringPeakHours(Autobus autobus)
        {

            // Obtén la hora actual
            DateTime horaActual = DateTime.Now;

            // Verifica si es horario pico (entre las 5 am y las 9 am o entre las 4 pm y las 8 pm)
            bool esHorarioPico = (horaActual.Hour >= 5 && horaActual.Hour < 9) || (horaActual.Hour >= 16 && horaActual.Hour < 20);

            if (esHorarioPico)
            {
                // Verifica si el autobús está en operación (ajusta el atributo según tus propias reglas)
                if (autobus.EnOperacion == true)
                {
                    // Si el autobús está en operación durante el horario pico, retorna true
                    return true;
                }
            }

            // Si no es horario pico o el autobús no está en operación, retorna false
            return false;
        }
    }
}
