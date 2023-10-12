using System;
using System.Collections.Generic;
using Transportadora.Interfaces;
using Transportadora.Models;
using Transportadora.Repositories;

namespace Transportadora.Services
{
    public class CargadorService : ICargadorService
    {
        private readonly CargadorRepository _cargadorRepository;

        public CargadorService(CargadorRepository cargadorRepository)
        {
            _cargadorRepository = cargadorRepository;
        }

        public void CreateCargador(Cargador cargador)
        {
            _cargadorRepository.CreateCargador(cargador);
        }

        public void DeleteCargador(int id)
        {
            _cargadorRepository.DeleteCargador(id);
        }

        public List<Cargador> GetAllCargadores()
        {
            return _cargadorRepository.GetAllCargadores();
        }

        public Cargador GetCargadorById(int id)
        {
            return _cargadorRepository.GetCargadorById(id);
        }

        public bool IsCargadorInUseDuringPeakHours(Cargador cargador)
        {
            DateTime horaActual = DateTime.Now;
            bool esHorarioPico = (horaActual.Hour >= 5 && horaActual.Hour < 9) || (horaActual.Hour >= 16 && horaActual.Hour < 20);

            return esHorarioPico && cargador.EnUso;
        }

        public void UpdateCargador(Cargador cargador)
        {
            _cargadorRepository.UpdateCargador(cargador);
        }

        
    }
}

