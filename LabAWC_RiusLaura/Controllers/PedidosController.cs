using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante_API.Controllers.Responses;
using Restaurante_API.DTO;
using Serilog;
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

        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("GetPedidoBy/{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            // Verificar si el ID proporcionado es mayor que 0
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID proporcionado no es válido. Debe ser un número mayor que 0.",
                    Data = null
                });
            }
            try
            {
                // Guarda el pedido en una variable que va a llamar al Servicio 
                var pedido = await _pedidoService.GetPedidoById(id);
                if (pedido == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"No se encontró un pedido con el ID {id}.",
                        Data = null
                    });
                }
                // Si se encuentra el pedido, devolverlo con un código de estado 200 OK
                return Ok(new ApiResponse<PedidoResponseDto>
                {
                    Success = true,
                    Message = "Pedido obtenido exitosamente.",
                    Data = pedido
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al buscar el pedido: {ex.Message}",
                    Data = null
                });
            }

        }

        //// INFORMES PEDIDOS A -  producto más vendido
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("GetProductoMasVendido")]

        public async Task<IActionResult> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                // Llama al servicio para obtener el producto más vendido con el filtro de fechas
                var productoMasVendido = await _pedidoService.GetProductoMasVendido(fechaInicio, fechaFin);

                // Si el producto no existe en la base de datos, devuelve un mensaje de error
                if (productoMasVendido == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se encontró ningún producto vendido.",
                        Data = null
                    });
                }

                // Devuelve el producto más vendido con un código de estado 200 OK
                return Ok(new ApiResponse<ProductoVendidoDto>
                {
                    Success = true,
                    Message = "Producto más vendido obtenido exitosamente.",
                    Data = productoMasVendido
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al obtener el producto más vendido: {ex.Message}",
                    Data = null
                });
            }
        }


        // INFORMES PEDIDOS B -  producto menos vendido
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("GetProductoMenosVendido")]
        public async Task<IActionResult> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                // Llama al servicio para obtener el producto menos vendido, pasando las fechas como parámetros
                var productoMenosVendido = await _pedidoService.GetProductoMenosVendido(fechaInicio, fechaFin);

                // Si no se encuentra ningún producto menos vendido, devuelve un mensaje de error
                if (productoMenosVendido == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se encontró ningún producto vendido.",
                        Data = null
                    });
                }

                // Devuelve el producto menos vendido con un código de estado 200 OK
                return Ok(new ApiResponse<ProductoVendidoDto>
                {
                    Success = true,
                    Message = "Producto menos vendido obtenido exitosamente.",
                    Data = productoMenosVendido
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al obtener el producto menos vendido: {ex.Message}",
                    Data = null
                });
            }
        }


        // POST Crear un pedido nuevo
        [Authorize(Policy = "RequireMozoRole")]
        [HttpPost("CrearPedido")]
        public async Task<ActionResult<PedidoResponseDto>> CrearPedido([FromBody] PedidoCreateDto pedido)
        {

            // Verifica que Comanda, Producto y Cantidad sean válidos y no estén vacíos
            if (pedido.ComandaId <= 0 || pedido.ProductoId <= 0 || pedido.Cantidad <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "ComandaId, ProductoId y Cantidad son obligatorios y deben ser mayores a 0.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para crear el nuevo pedido
                var nuevoPedido = await _pedidoService.CrearPedido(pedido);

                // Si el pedido no se pudo crear devuelve un mensaje de error
                if (nuevoPedido == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se pudo crear el pedido ya que la Comanda o el Producto no fueron encontrados.",
                        Data = null
                    });
                }

                // Devuelve el nuevo pedido con un código de estado 200 OK
                return Ok(new ApiResponse<PedidoResponseDto>
                {
                    Success = true,
                    Message = "Pedido creado exitosamente.",
                    Data = nuevoPedido
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al crear el pedido: {ex.Message}",
                    Data = null
                });
            }
        }

        [Authorize]
        [HttpGet("GetProductosEnEstadoPendientePorSector")]
        public async Task<IActionResult> GetProductosxSector(int sectorId)
        {
            
            // Obtenemos el rol del usuario autenticado
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userSectorIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SectorId")?.Value;

            // Si el usuario no tiene el claim de sector o rol, retornamos no autorizado
            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userSectorIdClaim))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No tiene permisos para realizar esta operación.",
                    Data = null
                });
            }

            // Convertimos el claim de sector a entero
            if (!int.TryParse(userSectorIdClaim, out var userSectorId))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "No tiene permisos para realizar esta operación.",
                    Data = null
                });
            }

            // Verificamos permisos según el rol
            if (userRole != "Socio" && userSectorId != sectorId)
            {
                return StatusCode(403, new ApiResponse<object>
                {
                    Success = false,
                    Message = "No tiene permisos para visualizar los pedidos del sector solicitado.",
                    Data = null
                });
            }

            // Validar que el sectorId sea mayor a 0
            if (sectorId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID del sector proporcionado no es válido. Debe ser un número mayor que 0.",
                    Data = null
                });
            }

            try
            {
                // Llamamos al servicio para obtener los productos por sector en estado pendiente
                var productos = await _pedidoService.GetProductosPendientesXSector(sectorId);

                // Si no hay productos, retornamos un mensaje de error
                if (productos == null || !productos.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se encontró ningún producto en estado pendiente para este sector.",
                        Data = null
                    });
                }
                return Ok(new ApiResponse<List<ProductoPendienteDto>>
                {
                    Success = true,
                    Message = "Productos en estado pendiente encontrados exitosamente.",
                    Data = productos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener los productos pendientes: {ex.Message}",
                    Data = null
                });
            }
        }


        //[Authorize(Policy = "RequireSocioRole")]
        //[HttpGet("GetProductosEnEstadoPendientePorSector")]
        //public async Task<IActionResult> GetProductosxSector(int sectorId)
        //{
        //    // Obtenemos el rol del usuario autenticado
        //    var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        //    var userSectorIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SectorId")?.Value;

        //    // Si el usuario no tiene el claim de sector o rol, retornamos no autorizado
        //    if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userSectorIdClaim))
        //    {
        //        return Unauthorized(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "No tiene permisos para realizar esta operación.",
        //            Data = null
        //        });
        //    }

        //    // Convertimos el claim de sector a entero
        //    if (!int.TryParse(userSectorIdClaim, out var userSectorId))
        //    {
        //        return Unauthorized(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "No tiene permisos para realizar esta operación.",
        //            Data = null
        //        });
        //    }

        //    // Verificamos permisos según el rol
        //    if (userRole != "Socio" && userSectorId != sectorId)
        //    {
        //        return StatusCode(403, new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "No tiene permisos para visualizar los pedidos del sector solicitado.",
        //            Data = null
        //        });
        //    }

        //    // Validar que el sectorId sea mayor a 0
        //    if (sectorId <= 0)
        //    {
        //        return BadRequest(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "El ID del sector proporcionado no es válido. Debe ser un número mayor que 0.",
        //            Data = null
        //        });
        //    }

        //    try
        //    {
        //        // Llama al servicio para obtener los productos por sector en estado pendiente
        //        var productos = await _pedidoService.GetProductosPendientesXSector(sectorId);

        //        // Si no hay productos, retorna un mensaje de error
        //        if (productos == null || !productos.Any())
        //        {
        //            return NotFound(new ApiResponse<object>
        //            {
        //                Success = false,
        //                Message = "No se encontró ningún producto en estado pendiente para este sector.",
        //                Data = null
        //            });
        //        }
        //        return Ok(new ApiResponse<List<ProductoPendienteDto>>
        //        {
        //            Success = true,
        //            Message = "Productos en estado pendiente encontrados exitosamente.",
        //            Data = productos
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ocurrió un error al obtener el producto: {ex.Message}");

        //    }

        //}

    }

}
