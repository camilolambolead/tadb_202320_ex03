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

        public List<Cargador> GetAllCargadores()
        {
            return _cargadorRepository.GetAllCargadores();
        }



        private bool IsHoraPico(DateTime hora)
        {
            // Define las horas pico de acuerdo a las restricciones (de 5 am a 9 am y de 4 pm a 8 pm)
            return (hora.Hour >= 5 && hora.Hour < 9) || (hora.Hour >= 16 && hora.Hour < 20);
        }
    }
}

