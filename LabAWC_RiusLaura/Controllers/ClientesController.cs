using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClienteServicio _clienteServicio;

        public ClientesController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        

        [HttpGet("GetDemoraV2")]
        public async Task<IActionResult> GetDemoraV2([Required] string codigoMesa, [Required] string CodigoCliente)
        {
            try
            {
                // Llamada al servicio para obtener la demora
                var resultado = await _clienteServicio.GetDemoraV2(codigoMesa, CodigoCliente);

                // Devolver resultado exitoso
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra la mesa o el pedido
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el códigocliente  no está asociado a la mesa
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
    }

    
}

