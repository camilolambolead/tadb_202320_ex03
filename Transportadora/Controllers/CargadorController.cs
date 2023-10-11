using Microsoft.AspNetCore.Mvc;
using Transportadora.Repositories;
using Transportadora.Services;
using Transportadora.Models;
using System;
using System.Collections.Generic;


namespace Transportadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargadorController : ControllerBase
    {
       
       
            private readonly CargadorRepository _cargadorRepository;

            public CargadorController(CargadorRepository cargadorRepository)
            {
                _cargadorRepository = cargadorRepository;
            }

            // Obtener todos los cargadores
            [HttpGet]
            public ActionResult<IEnumerable<Controllers.CargadorController>> GetCargadores()
            {
                var cargadores = _cargadorRepository.GetAllCargadores();
                return Ok(cargadores);
            }

            // Obtener un cargador por su Id
            [HttpGet("{id}")]
            public ActionResult<Controllers.CargadorController> GetCargador(int id)
            {
                var cargador = _cargadorRepository.GetCargadorById(id);
                if (cargador == null)
                {
                    return NotFound();
                }
                return Ok(cargador);
            }

            // Crear un nuevo cargador
            [HttpPost]
            public ActionResult<Controllers.CargadorController> CreateCargador([FromBody] Models.Cargador cargador)

            {
                // Validar que no se esté utilizando un cargador en horario pico
                if (_cargadorRepository.IsCargadorInUseDuringPeakHours(cargador))
                {
                    return BadRequest("El cargador no puede utilizarse en horario pico.");
                }

                _cargadorRepository.CreateCargador(cargador);
                return CreatedAtAction(nameof(GetCargador), new { id = cargador.Id }, cargador);
            }

            // Actualizar un cargador existente
            [HttpPut("{id}")]
            public IActionResult UpdateCargador(int id, [FromBody] Models.Cargador cargador)
            {
                if (id != cargador.Id)
                {
                    return BadRequest("ID de cargador no válido.");
                }

                // Validar que no se esté utilizando un cargador en horario pico
                if (_cargadorRepository.IsCargadorInUseDuringPeakHours(cargador))
                {
                    return BadRequest("El cargador no puede utilizarse en horario pico.");
                }

                _cargadorRepository.UpdateCargador(cargador);
                return NoContent();
            }

            // Eliminar un cargador existente
            [HttpDelete("{id}")]
            public IActionResult DeleteCargador(int id)
            {
                var cargador = _cargadorRepository.GetCargadorById(id);
                if (cargador == null)
                {
                    return NotFound();
                }

                _cargadorRepository.DeleteCargador(id);
                return NoContent();
            }
        
    }
}
