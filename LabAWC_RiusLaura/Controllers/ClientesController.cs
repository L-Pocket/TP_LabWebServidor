using LabAWC_RiusLaura.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DataContext _context;

        // Constructor
        public ClientesController(DataContext context)
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
        }
    }
}

