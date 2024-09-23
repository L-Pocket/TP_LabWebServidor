using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("GetPedidoBy/{id}")]
        public async Task<ActionResult<PedidoResponseDto>> GetPedidoById(int id)
        {
            // Verificar si el ID proporcionado es mayor que 0
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido. Debe ser un número mayor que 0.");
            }
            try
            {
                // Guarda el pedido en una variable que va a llamar al Servicio 
                var pedido = await _pedidoService.GetPedidoById(id);
                if (pedido == null)
                {
                    return NotFound($"Pedido con ID {id} no encontrado.");
                }
                // Si se encuentra el pedido, devolverlo con un código de estado 200 OK
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al buscar el pedido: {ex.Message}");
            }

        }

        //// GET Lo que MÁS se vendió.
        [HttpGet("GetProductoMasVendido")]
        public async Task<IActionResult> GetProductoMasVendido()
        {
            try
            {
                // Llama al servicio para obtener el producto más vendido
                var productoMasVendido = await _pedidoService.GetProductoMasVendido();

                // Si el producto no existe en la base de datos, devuelve un mensaje de error
                if (productoMasVendido == null)
                {

                    return NotFound("No se encontró ningún producto vendido.");
                }

                // Devuelve el producto más vendido con un código de estado 200 OK
                return Ok(productoMasVendido);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocurrió un error al obtener el producto más vendido: {ex.Message}");
            }
        }

        // GET Lo que MENOS se vendió.
        [HttpGet("GetProductoMenosVendido")]
        public async Task<IActionResult> GetProductoMenosVendido()
        {
            try
            {
                // Llama al servicio para obtener el producto menos vendido
                var productoMenosVendido = await _pedidoService.GetProductoMenosVendido();

                // Si no se encuentra ningún producto menos vendido, devuelve un mensaje de error
                if (productoMenosVendido == null)
                {
                    return NotFound("No se encontró ningún producto vendido.");
                }

                // Devuelve el producto menos vendido con un código de estado 200 OK
                return Ok(productoMenosVendido);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocurrió un error al obtener el producto menos vendido: {ex.Message}");
            }
        }

        // POST Crear un pedido nuevo
        [HttpPost("CrearPedido")]
        public async Task<ActionResult<PedidoResponseDto>> CrearPedido([FromBody] PedidoCreateDto pedido)
        {

            // Verifica que Comanda, Producto y Cantidad sean válidos y no estén vacíos
            if (pedido.ComandaDelPedidoId <= 0 || pedido.ProductoDelPedidoId <= 0 || pedido.Cantidad <= 0)
            {
                return BadRequest("ComandaDelPedidoId, ProductoDelPedidoId, Cantidad son obligatorios y deben ser válidos.");
            }

            // Verificación de CodigoCliente
            if (string.IsNullOrEmpty(pedido.CodigoCliente) || pedido.CodigoCliente.Length != 5)
            {
                return BadRequest("CodigoCliente es obligatorio y debe tener exactamente 5 caracteres.");
            }

            try
            {
                // Llama al servicio para crear el nuevo pedido
                var nuevoPedido = await _pedidoService.CrearPedido(pedido);

                // Si el pedido no se pudo crear devuelve un mensaje de error
                if (nuevoPedido == null)
                {
                    return NotFound("No se pudo crear el pedido ya que la Comanda o el Producto no fueron encontrados.");
                }

                // Devuelve el nuevo pedido con un código de estado 200 OK
                return Ok(nuevoPedido);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }

        [HttpGet("GetProductos en estado pendiente por sector")]
        public async Task<ActionResult<List<Producto>>> GetProductosxSector(int sectorI)
        {
            try
            {
                var productos = await _pedidoService.GetAllProductosXSector(sectorI);
                if (productos == null || !productos.Any())
                {
                    return NotFound("No se encontró ningún producto vendido para este sector en estado pendiente.");
                }
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener el producto: {ex.Message}");

            }


        }


        // CODIGO VIEJO CON LÓGICA EN EL CONTROLLER ---------------
        //private readonly DataContext _context;

        //public PedidosController(DataContext context)
        //{
        //    _context = context;
        //}


        //// GET de todos los pedidos, historial 
        //[HttpGet("GetPedidos")]
        //public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        //{
        //    try
        //    {
        //        // Obtiene la lista de todos los pedidos desde la BBDD
        //        var pedidos = await _context.Pedidos.ToListAsync();

        //        // Verifica si la lista está vacía
        //        if (pedidos == null || !pedidos.Any()) // devuelve true si la lista está vacía o es null.
        //        {
        //            return NoContent();
        //        }

        //        // Retorna la lista de pedidos con un código de estado 200 OK
        //        return Ok(pedidos);
        //    }
        //    catch (Exception ex)
        //    {                
        //        return StatusCode(500, $"Error al obtener los pedidos: {ex.Message}");
        //    }
        //}

        // GET de 1 pedido por su ID 

        //[HttpGet("GetPedidoBy/{id}")]
        //public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        //{
        //    // Verificar si el ID proporcionado es mayor que 0
        //    if (id <= 0)
        //    {
        //        return BadRequest("El ID proporcionado no es válido. Debe ser un número mayor que 0.");
        //    }

        //    try
        //    {
        //        // Intentar encontrar el pedido en la BBDD utilizando el ID proporcionado
        //        var pedido = await _context.Pedidos.FindAsync(id);

        //        if (pedido == null)
        //        {
        //            return NotFound($"No se encontró un pedido con el ID {id}.");
        //        }

        //        // Si se encuentra el pedido, devolverlo con un código de estado 200 OK
        //        return Ok(pedido);
        //    }
        //    catch (Exception ex)
        //    {                
        //        return StatusCode(500, $"Ocurrió un error al buscar el pedido: {ex.Message}");
        //    }
        //}

        //// GET Lo que MÁS se vendió.
        //[HttpGet("GetProductoMasVendido")]
        //public async Task<IActionResult> GetProductoMasVendido()
        //{
        //    try
        //    {
        //        // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
        //        var productoMasVendido = await _context.Pedidos
        //            .GroupBy(p => p.ProductoDelPedidoId)
        //            .Select(g => new  // Seleccionamos el Id del producto y la cantidad vendida
        //            {
        //                ProductoId = g.Key,
        //                CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
        //            })
        //            .OrderByDescending(g => g.CantidadVendida) // aca nos estaria ordenando de forma desc por la cantidad vendida para saber cual es el mas vendido
        //            .FirstOrDefaultAsync(); // obtenemos el primero resultado mas vendido o null si es que no hay datos 

        //        // Si no se encuentra ningún producto vendido, devuelve un mensaje de error
        //        if (productoMasVendido == null)
        //        {
        //            return NotFound("No se encontraron datos de ventas.");
        //        }

        //        // Busca el producto en la BBDD utilizando el ID obtenido
        //        var producto = await _context.Productos.FindAsync(productoMasVendido.ProductoId);

        //        // Si el producto no existe en la base de datos, devuelve un mensaje de error
        //        if (producto == null)
        //        {
        //            return NotFound($"No se encontró el producto con ID {productoMasVendido.ProductoId}.");
        //        }

        //        // Devuelve un objeto con los detalles del producto más vendido y la cantidad vendida
        //        return Ok(new
        //        {
        //            Producto = producto, // Detalles del producto que menos se vendio
        //            CantidadVendida = productoMasVendido.CantidadVendida  // aca vemos la cantidad vendida del producto
        //        });
        //    }
        //    catch (Exception ex)
        //    {                
        //        return StatusCode(500, $"Ocurrió un error al obtener el producto más vendido: {ex.Message}");
        //    }
        //}

        //// GET Lo que MENOS se vendió.
        //[HttpGet("GetProductoMenosVendido")]
        //public async Task<IActionResult> GetProductoMenosVendido()
        //{
        //    try
        //    {
        //        // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
        //        var productoMenosVendido = await _context.Pedidos
        //            .GroupBy(p => p.ProductoDelPedidoId) // estariamos agrupando por el Id del producto
        //            .Select(g => new  //Seleccionamos el Id del producto y la cantidad vendida
        //            {
        //                ProductoId = g.Key,
        //                CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
        //            })
        //            .OrderBy(g => g.CantidadVendida) // Ordenamos de forma asc por cantidad vendida para saber cual es el menos vendido 
        //            .FirstOrDefaultAsync(); // obtenemos el primero resultado menos vendido o null si es que no hay datos 

        //        // Si no se encuentra ningún producto vendido, devuelve un mensaje de error
        //        if (productoMenosVendido == null)
        //        {
        //            return NotFound("No se encontraron datos de ventas.");
        //        }

        //        // aca estamos cargando  los detalles del producto desde la tabla de productos 
        //        var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);

        //        // Si el producto no existe en la base de datos, devuelve un mensaje de error
        //        if (producto == null)
        //        {
        //            return NotFound($"No se encontró el producto con ID {productoMenosVendido.ProductoId}.");
        //        }

        //        return Ok(new
        //        {
        //            Producto = producto, // Detalles del producto que menos se vendio
        //            CantidadVendida = productoMenosVendido.CantidadVendida  // aca vemos la cantidad vendida del producto
        //        });
        //    }
        //    catch (Exception ex)            {

        //        return StatusCode(500, $"Ocurrió un error al obtener el producto menos vendido: {ex.Message}");
        //    }
        //}

        //// POST Crear un pedido nuevo
        //[HttpPost("CrearPedido")]
        //public async Task<ActionResult<Pedido>> CrearPedido(int comandaId, int productoId, int cantidad,int tiempoEstimado, string codigoCliente, string? observaciones = null)
        //{
        //    // Verifica que todos los parámetros sean válidos y no estén vacíos
        //    // No se pide el estado del Pedido, ya que se inicializa en 1 "Pendiente"
        //    if (comandaId <= 0 || productoId <= 0 || cantidad <= 0 || tiempoEstimado <= 0 || string.IsNullOrEmpty(codigoCliente))
        //    {
        //        return BadRequest("comandaId, productoID, cantidad, tiempoEstimado y codigoCliente son obligatorios y deben ser válidos.");
        //    }
        //    // Verificar si la comanda existe
        //    var comandaExistente = await _context.Comandas.FindAsync(comandaId);
        //    if (comandaExistente == null)
        //    {
        //        return NotFound($"La comanda con ID {comandaId} no existe.");
        //    }
        //    // Verificar si el producto existe
        //    var productoExistente = await _context.Productos.FindAsync(productoId);
        //    if (productoExistente == null)
        //    {
        //        return NotFound($"El producto con ID {productoId} no existe.");
        //    }

        //    try
        //    {
        //        // Crear una nueva instancia de Pedido con los parámetros proporcionados
        //        var nuevoPedido = new Pedido
        //        {
        //            ComandaDelPedidoId = comandaId,
        //            ProductoDelPedidoId = productoId,
        //            Cantidad = cantidad,
        //            EstadoDelPedidoId = 1, // se inicializa en 1 "Pendiente"
        //            FechaCreacion = DateTime.Now,
        //            TiempoEstimado = 0, // Inicializa en cero
        //            CodigoCliente = codigoCliente,
        //            ObservacionesDelPedido = observaciones
        //        };

        //        _context.Pedidos.Add(nuevoPedido); // Añadir el nuevo pedido a la base de datos
        //        await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

        //        // Devuelve un código de estado 200 OK con el pedido creado
        //        return Ok(nuevoPedido);
        //    }

        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
        //    }

        //}

    }
}
