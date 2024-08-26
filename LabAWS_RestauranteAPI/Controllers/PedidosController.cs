using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabAWS_RestauranteAPI.Models;
using LabAWS_RestauranteAPI.DAL;


namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly DataContext _context;
        private static List<Pedido> _pedidos = new List<Pedido>();

        public PedidosController(DataContext context)
        {
            _context = context;
        }


        // GET de todos los pedidos, historial 
        [HttpGet("GetPedidos")]

        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            var resultado = await _context.Pedidos.ToListAsync();
            if (resultado == null || resultado.Count == 0)
            {
                return NoContent();
            }
            return Ok(resultado);
        }

        // GET de 1 pedido por su ID 
        [HttpGet("GetPedidoBy/{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound($"No se encontró un pedido con el id {id}.");
            }

            return Ok(pedido);
        }

        // POST Crear un pedido nuevo
        //[HttpPost]
        //public async Task<IActionResult> CreatePedido( string descripcion, int cantidad, int idEstado,int demoraEstipulada, string? codigoCliente = null, string? observaciones = null)
        //{
        //    if (cantidad <= 0)
        //    {
        //        return BadRequest("La cantidad es obligatoria y debe ser mayor a cero.");
        //    }

        //    // aca lo esta creando pero falta la descripcion porque pedido no tiene descripcon pero si producto
        //    //falta que a la hora de tomar el pedido lo relacione con la comanda y id de producto
        //    //falta que pedido tenga una lista de productos
        //    var pedido = new Pedido
        //    {   
        //        Cantidad = cantidad,
        //        IdEstado = idEstado,
        //        FechaCreacion = DateTime.Now,
        //        CodigoCliente = codigoCliente,
        //        Observaciones = observaciones

        //    };
        //    try
        //    {
        //        _context.Pedidos.Add(pedido);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.IdPedido }, pedido);

        //    }

        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al guardar el pedido: {ex.Message}");
        //    }

        //}


        // GET
        // Lo que MÁS se vendió.
        [HttpGet("mas-vendido")]
        public async Task<IActionResult> GetProductoMasVendido()
        {
            var productoMasVendido = await _context.Pedidos
                .GroupBy(dp => dp.ProductoDelPedidoId)  // estariamos agrupando por el Id del producto
                .Select(g => new {              // Seleccionamos el Id del producto y la cantidad vendida
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(dp => dp.Cantidad)  // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderByDescending(g => g.CantidadVendida)  // aca nos estaria ordenando de forma desc por la cantidad vendida para saber cual es el mas vendido
                .FirstOrDefaultAsync();  // obtenemos el primero resultado mas vendido o null si es que no hay datos 

            if (productoMasVendido == null)
            {
                return NotFound("No se encontraron datos de ventas.");
            }

            // aca estamos cargando  los detalles del producto desde la tabla de productos 
            var producto = await _context.Productos.FindAsync(productoMasVendido.ProductoId);

            //return Ok(producto);  // devolvemos cual es el producto que mas se vendio 

            return Ok(new
            {
                Producto = producto,  // Detalles del producto que menos se vendio
                CantidadVendida = productoMasVendido.CantidadVendida  // aca vemos la cantidad vendida del producto
            });


        }


        // GET
        // Lo que MENOS se vendió.

        [HttpGet("menos-vendido")]
        public async Task<IActionResult> GetProductoMenosVendido()
        {
            var productoMenosVendido = await _context.Pedidos
                .GroupBy(dp => dp.ProductoDelPedidoId)  // estariamos agrupando por el Id del producto
                .Select(g => new {              //Seleccionamos el Id del producto y la cantidad vendida
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(dp => dp.Cantidad)  // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderBy(g => g.CantidadVendida)  // Ordenamos de forma asc por cantidad vendida para saber cual es el menos vendido 
                .FirstOrDefaultAsync();  // obtenemos el primero resultado menos vendido o null si es que no hay datos 

            if (productoMenosVendido == null)
            {
                return NotFound("No se encontraron datos de ventas.");
            }

            // aca estamos cargando  los detalles del producto desde la tabla de productos 
            var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);

            // return Ok(producto);  // devolvemos cual es el producto que menos se vendio 

            return Ok(new
            {
                Producto = producto,  // Detalles del producto que menos se vendio
                CantidadVendida = productoMenosVendido.CantidadVendida  // aca vemos la cantidad vendida del producto
            });
        }



    }
}

