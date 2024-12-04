using Entidades;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public EmpleadosController(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        [HttpPut("PonerPedidoEnPreparacion/{idPedido}")]
        public async Task<ActionResult> PonerPedidoEnPreparacion(int idPedido, [FromQuery] int tiempoEstimado)
        {
            // Verifica que todos los parámetros sean válidos
            if (idPedido <= 0 || tiempoEstimado <= 0)
            {
                return BadRequest("Los parámetros 'idPedido' y 'tiempoEstimado' son obligatorios y deben ser mayores que cero.");
            }

            try
            {
                // Llama al servicio para poner el pedido en preparación
                var resultado = await _empleadoServicio.PonerPedidoEnPreparacion(idPedido, tiempoEstimado);
                
                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en pendiente 
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("PonerPedidoListoParaServir/{idPedido}")]
        public async Task<ActionResult> PonerPedidoListoParaServir(int idPedido)
        {           
            
            // Verifica que todos los parámetros sean válidos
            if (idPedido <= 0)
            {
                return BadRequest("idPedidoes obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para poner el pedido listo para servir
                var resultado = await _empleadoServicio.PonerPedidoListoParaServir(idPedido);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en preparación
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("CambiarEstadoMesaClienteComiendo/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClienteComiendo(int idMesa)
        {
            
            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest("idMesa es obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Comiendo
                var resultado = await _empleadoServicio.CambiarEstadoMesaClienteComiendo(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
                //return Ok($"OK! La mesa con el ID {idMesa} tiene a sus clientes comiendo");
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Esperando Pedido
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("CambiarEstadoMesaClientePagando/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClientePagando(int idMesa)
        {
            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest("idMesa es obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Pagando
                var resultado = await _empleadoServicio.CambiarEstadoMesaClientePagando(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
                //return Ok($"OK! La mesa con el ID {idMesa} está pagando");
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Comiendo
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



