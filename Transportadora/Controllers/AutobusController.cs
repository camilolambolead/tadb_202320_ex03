using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Transportadora.Interfaces;
using Transportadora.Models;
using Transportadora.Services;

namespace Transportadora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutobusController : ControllerBase
    {
        private readonly IAutobusService _autobusService;

        public AutobusController(IAutobusService autobusService)
        {
            _autobusService = autobusService;
        }

        // Obtener todos los autobuses
        [HttpGet]
        public ActionResult<IEnumerable<Autobus>> GetAutobuses()
        {
            var autobuses = _autobusService.GetAllAutobuses();
            return Ok(autobuses);
        }

        // Obtener un autobús por su ID
        [HttpGet("{id}")]
        public ActionResult<Autobus> GetAutobus(int id)
        {
            var autobus = _autobusService.GetAutobusById(id);
            if (autobus == null)
            {
                return NotFound();
            }
            return Ok(autobus);
        }

        // Crear un nuevo autobús
        [HttpPost]
        public ActionResult<Autobus> CreateAutobus([FromBody] Autobus autobus)
        {
            _autobusService.CreateAutobus(autobus);
            return CreatedAtAction(nameof(GetAutobus), new { id = autobus.Id }, autobus);
        }

        // Actualizar un autobús existente
        [HttpPut("{id}")]
        public IActionResult UpdateAutobus(int id, [FromBody] Autobus autobus)
        {
            if (id != autobus.Id)
            {
                return BadRequest("ID de autobús no válido.");
            }

            _autobusService.UpdateAutobus(autobus);
            return NoContent();
        }

        // Eliminar un autobús existente
        [HttpDelete("{id}")]
        public IActionResult DeleteAutobus(int id)
        {
            var autobus = _autobusService.GetAutobusById(id);
            if (autobus == null)
            {
                return NotFound();
            }

            _autobusService.DeleteAutobus(id);
            return NoContent();
        }
    }
}
