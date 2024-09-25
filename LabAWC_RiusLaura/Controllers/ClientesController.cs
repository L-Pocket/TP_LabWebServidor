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

        //[HttpGet("GetDemora")]
        //public async Task<IActionResult> GetDemora(string codigoMesa, string idPedido)
        //{
        //    var (success, errorMessage, mesa, pedido, resultado) = await _clienteServicio.GetDemora(codigoMesa, idPedido);

        //    if (!success)
        //    {
        //        if (mesa == null)
        //        {
        //            return NotFound(errorMessage);
        //        }

        //        return BadRequest(errorMessage);
        //    }

        //    return Ok(resultado);
        //}

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

    // CODIGO VIEJO con lógica en el controller
    //private readonly DataContext _context;
    // Constructor
    /*public ClientesController(DataContext context)
    {
        _context = context;
    }

    // GET
    // El cliente ingresa el código de la mesa junto con el número de pedido y ve el tiempo de demora de su pedido.
    [HttpGet("TiempoDeDemora/{codigoMesa}/{idPedido}")]
    public async Task<ActionResult<object>> ObtenerTiempoDeDemora(string codigoMesa, string idPedido)
    {
        try
        {
            // Buscar la mesa por código en la BBDD
            var mesa = await _context.Mesas
                .Where(m => m.CodigoMesa == codigoMesa)
                .FirstOrDefaultAsync();

            if (mesa == null)
            {
                return NotFound($"No se encontró una mesa con el código {codigoMesa}.");
            }

            // Buscar el pedido por código en la BBDD
            var pedido = await _context.Pedidos
                .Where(p => p.CodigoCliente == idPedido)
                .FirstOrDefaultAsync();

            if (pedido == null)
            {
                return NotFound($"No se encontró un pedido con el ID {idPedido}.");
            }

            // Verificar que el pedido esté asociado a la mesa correcta                
            var comanda = await _context.Comandas
                .Where(c => c.IdComanda == pedido.ComandaDelPedidoId && c.MesaDeComandaId == mesa.IdMesa)
                .FirstOrDefaultAsync();

            if (comanda == null)
            {
                return BadRequest("El pedido no está asociado con la mesa proporcionada.");
            }

            var tiempoReal = (DateTime.Now - pedido.FechaCreacion).TotalMinutes;
            var tiempoEstimado = pedido.TiempoEstimado;

            var resultado = new
            {                    
                TiempoEstimado = tiempoEstimado,                    
                Su_Pedido_Esta_Demorado = Math.Round(tiempoReal - tiempoEstimado, 0) // Redondear a cero decimales
            };

            return Ok(resultado);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }*/
}

