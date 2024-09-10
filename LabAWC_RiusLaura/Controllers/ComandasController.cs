using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly IComandaServicio _comandaServicio;

        public ComandasController(IComandaServicio comandaServicio)
        {
            _comandaServicio = comandaServicio;
        }

        [HttpPost("CrearComanda")]
        public async Task<IActionResult> CrearComanda([FromQuery] ComandaDto comandaDto)
        {
            var (success, errorMessage, nuevaComanda) = await _comandaServicio.CrearComanda(comandaDto);

            if (!success)
            {
                return StatusCode(500, errorMessage);
            }

            return CreatedAtAction(nameof(ObtenerComandaPorId), new { id = nuevaComanda.IdComanda }, nuevaComanda);
        }


        [HttpGet("ObtenerTodasLasComandas")]
        public async Task<IActionResult> ObtenerTodasLasComandas()
        {
            var (success, errorMessage, comandas) = await _comandaServicio.ObtenerTodasLasComandas();

            if (!success)
            {
                return StatusCode(500, errorMessage);
            }

            return Ok(comandas);
        }

        [HttpGet("ObtenerComandaPorId/{id}")]
        public async Task<IActionResult> ObtenerComandaPorId(int id)
        {
            var (success, errorMessage, comanda) = await _comandaServicio.ObtenerComandaPorId(id);

            if (!success)
            {
                return NotFound(errorMessage);
            }

            return Ok(comanda);
        }

        [HttpPut("ModificarComanda/{idComanda}")]
        public async Task<IActionResult> ModificarComanda(int idComanda, [FromQuery] ComandaDto comandaDto)
        {
            var (success, errorMessage) = await _comandaServicio.ModificarComanda(idComanda, comandaDto);

            if (!success)
            {
                return NotFound(errorMessage);
            }

            return NoContent();
        }

    }
}
