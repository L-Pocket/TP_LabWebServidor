using Entidades;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante_API.Controllers.Responses;
using Restaurante_API.DTO;
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

        [Authorize(Policy = "RequireBartenderOrCerveceroOrCocineroRole")]
        [HttpPut("PonerPedidoEnPreparacion/{idPedido}")]
        public async Task<ActionResult> PonerPedidoEnPreparacion(int idPedido, [FromQuery] int tiempoEstimado)
        {
            // Verifica que todos los parámetros sean válidos            
            if (idPedido <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El parámetro 'idPedido' debe ser mayor que cero.",
                    Data = null
                });
            }

            if (tiempoEstimado <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El parámetro 'tiempoEstimado' debe ser mayor que cero.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para poner el pedido en preparación
                var resultado = await _empleadoServicio.PonerPedidoEnPreparacion(idPedido, tiempoEstimado);

                // Devuelve un código de estado 200 OK 
                return Ok(new ApiResponse<PedidoEstadoResponseDto>
                {
                    Success = true,
                    Message = "El pedido se ha puesto en preparación exitosamente.",
                    Data = resultado
                });
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El pedido especificado no existe.",
                    Data = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en pendiente 
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El pedido no se encuentra en estado pendiente.",
                    Data = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado al intentar poner el pedido en preparación.",
                    Data = ex.Message
                });
            }
        }
        
        [Authorize(Policy = "RequireBartenderOrCerveceroOrCocineroRole")]
        [HttpPut("PonerPedidoListoParaServir/{idPedido}")]
        public async Task<ActionResult> PonerPedidoListoParaServir(int idPedido)
        {

            // Verifica que todos los parámetros sean válidos
            if (idPedido <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El parámetro 'idPedido' es obligatorio y debe ser mayor que cero.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para poner el pedido listo para servir
                var resultado = await _empleadoServicio.PonerPedidoListoParaServir(idPedido);

                // Devuelve un código de estado 200 OK 
                return Ok(new ApiResponse<PedidoEstadoResponseDto>
                {
                    Success = true,
                    Message = "El pedido se ha puesto como listo para servir exitosamente.",
                    Data = resultado
                });
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El pedido especificado no existe.",
                    Data = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en preparación
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El pedido no se encuentra en estado de preparación.",
                    Data = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado al intentar marcar el pedido como listo para servir.",
                    Data = ex.Message
                });
            }
        }

        [Authorize(Policy = "RequireMozoRole")]
        [HttpPut("CambiarEstadoMesaClienteComiendo/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClienteComiendo(int idMesa)
        {

            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El parámetro 'idMesa' es obligatorio y debe ser mayor que cero.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Comiendo
                var resultado = await _empleadoServicio.CambiarEstadoMesaClienteComiendo(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(new ApiResponse<MesaDto>
                {
                    Success = true,
                    Message = $"La mesa con ID {idMesa} ahora está en estado 'Cliente Comiendo'.",
                    Data = resultado
                });
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "No se encontró la mesa especificada.",
                    Data = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Esperando Pedido
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "La mesa no está en el estado adecuado para cambiar a 'Cliente Comiendo'.",
                    Data = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado al intentar cambiar el estado de la mesa.",
                    Data = ex.Message
                });
            }
        }

        [Authorize(Policy = "RequireMozoRole")]
        [HttpPut("CambiarEstadoMesaClientePagando/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClientePagando(int idMesa)
        {
            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "El parámetro 'idMesa' es obligatorio y debe ser mayor que cero.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Pagando
                var resultado = await _empleadoServicio.CambiarEstadoMesaClientePagando(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"La mesa con ID {idMesa} ahora está en estado 'Cliente Pagando'.",
                    Data = resultado
                });
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "No se encontró la mesa especificada.",
                    Data = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Comiendo
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "La mesa no está en el estado adecuado para cambiar a 'Cliente Pagando'.",
                    Data = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado al intentar cambiar el estado de la mesa.",
                    Data = ex.Message
                });
            }
            
            
        }
    }
}



