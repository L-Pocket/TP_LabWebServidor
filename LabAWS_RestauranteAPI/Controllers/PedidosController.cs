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
        public PedidosController(RestauranteContext context)
        {
            _context = context;
        }
        //private static List<Pedido> _pedidos = null;


        private static List<Pedido> _pedidos = new List<Pedido>();
        [HttpGet]

        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            return Ok(await _context.Pedidos.ToListAsync());
        }

        // Requerimiento: Un mozo toma el pedido de una...
        [HttpPost]
        public async Task<IActionResult> CreatePedido(int idComanda,int idProducto,int cantidad,int idEstado, string? codigoCliente = null,string? observaciones = null)
        {
            if (cantidad <= 0)
            {
                return BadRequest("La cantidad es obligatoria y debe ser mayor a cero.");
            }

            // aca lo esta creando
            var pedido = new Pedido
            {
                IdPedido = _pedidos.Any() ? _pedidos.Max(x => x.IdPedido) + 1 : 1,
                IdComanda = idComanda,
                IdProducto = idProducto,
                Cantidad = cantidad,
                IdEstado = idEstado,
                FechaCreacion = DateTime.Now,
                CodigoCliente = codigoCliente,
                Observaciones = observaciones
            };

           
            _pedidos.Add(pedido);//aca lo agrega
            //_context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();// se lo agregue recien

            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.IdPedido }, pedido);
        }


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

