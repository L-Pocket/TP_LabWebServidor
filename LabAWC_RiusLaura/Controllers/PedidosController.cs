using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Security.Claims;

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
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("GetProductoMasVendido")]
        public async Task<IActionResult> GetProductoMasVendido()
        {
            try
            {
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userRole != "Socio")
                {
                    return Forbid("No tienes permiso para acceder a este recurso.");
                }
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
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("GetProductoMenosVendido")]
        public async Task<IActionResult> GetProductoMenosVendido()
        {
            try
            {
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userRole != "Socio")
                {
                    return Forbid("No tienes permiso para acceder a este recurso.");
                }
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


    }
    
}
