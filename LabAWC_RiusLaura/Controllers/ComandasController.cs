using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Mvc;
using Restaurante_API.DTO;
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
        public async Task<IActionResult> CrearComanda([FromBody] ComandaCrearDto comandacrearDto)
        {
            var (success, errorMessage, nuevaComanda) = await _comandaServicio.CrearComanda(comandacrearDto);

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
            // Verificamos si el ID es mayor que 0
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado debe ser mayor que 0.");
            }

            var (success, errorMessage, comanda) = await _comandaServicio.ObtenerComandaPorId(id);

            if (!success)
            {
                return NotFound(errorMessage);
            }

            return Ok(comanda);
        }


        [HttpPut("ModificarComanda/{idComanda}")]
        public async Task<IActionResult> ModificarComanda(int idComanda, [FromBody] ComandaCrearDto comandaCrearDto)

        {
            // Validar que el idComanda sea mayor a 0
            if (idComanda <= 0)
            {
                return BadRequest("El ID de la comanda debe ser un número mayor a 0.");
            }

            if (comandaCrearDto == null)
            {
                return BadRequest("La información de la comanda no puede estar vacía.");
            }

            var result = await _comandaServicio.ModificarComanda(idComanda, comandaCrearDto);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok("Comanda modificada exitosamente.");
        }


        /*
        [HttpPut("ModificarComanda/{idComanda}")]
        public async Task<IActionResult> ModificarComanda(int idComanda, [FromBody] ComandaDto comandaDto)
        {
            var (success, errorMessage) = await _comandaServicio.ModificarComanda(idComanda, comandaDto);

            if (!success)
            {
                return NotFound(errorMessage); // Devuelve un 404 si no se encuentra la comanda
            }

           
            return Ok(new { mensaje = "Comanda modificada con éxito" });
        }
        */
    }
}
