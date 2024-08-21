using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabAWS_RestauranteAPI.Models;


namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly RestauranteContext _context;
        private static List<Pedido> _pedidos = new List<Pedido>();

        public PedidosController(RestauranteContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            return Ok(await _context.Pedidos.ToListAsync());
        }

        // Requerimiento: Un mozo toma el pedido de una...
        // IActionResult proporciona más flexibilidad en el caso de cambiar el tipo de resultado 
        [HttpPost]
        public async Task<IActionResult> CreatePedido( string descripcion, int cantidad, int idEstado,int demoraEstipulada, string? codigoCliente = null, string? observaciones = null)
        {
            if (cantidad <= 0)
            {
                return BadRequest("La cantidad es obligatoria y debe ser mayor a cero.");
            }

            // aca lo esta creando pero falta la descripcion porque pedido no tiene descripcon pero si producto
            //falta que a la hora de tomar el pedido lo relacione con la comanda y id de producto
            //falta que pedido tenga una lista de productos
            var pedido = new Pedido
            {   
                Cantidad = cantidad,
                IdEstado = idEstado,
                FechaCreacion = DateTime.Now,
                CodigoCliente = codigoCliente,
                Observaciones = observaciones

            };
            try
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.IdPedido }, pedido);

            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el pedido: {ex.Message}");
            }

        }

        // este hay que reemplazarlo por codigo cliente?

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound($"No se encontró un pedido con el id {id}.");
            }

            return Ok(pedido);
        }

        // GET
        // Lo que MÁS se vendió.

        // GET
        // Lo que MENOS se vendió.

        // GET
        // Los que no se entregaron en el tiempo estipulado.
        // Faltaría una FECHA ESTIMADA en Pedido para comparar con el resultado de f.finalización - f.creacion
        // INVESTIGAR que tipo de dato se podría obtener de un cálculo de fechas.


    }
}

