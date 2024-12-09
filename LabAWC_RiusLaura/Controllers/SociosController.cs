using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LabAWS_RiusLaura.Servicios;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LabAWS_RiusLaura.DTO;
using Restaurante_API.DTO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Restaurante_API.Controllers.Responses;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioController : ControllerBase
    {
        private readonly ISocioServicio _socioServicio;

        public SocioController(ISocioServicio socioServicio)
        {
            _socioServicio = socioServicio;
        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpPut("CerrarMesa/{idMesa}")]
        public async Task<ActionResult<MesaDto>> CerrarMesa(int idMesa)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Socio")
            {
                return Forbid("No tienes permiso para acceder a este recurso.");
            }
            // Verifica que id mesa sea válido
            if (idMesa <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID de la mesa proporcionado no es válido. Debe ser un número mayor que 0.",
                    Data = null
                });
            }
            try
            {
                // Llama al servicio para modificar mesa
                var resultado = await _socioServicio.CerrarMesa(idMesa);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"No se encontró una mesa con el ID {idMesa}, o no está en un estado válido para cerrarla.",
                        Data = null
                    });
                }

                // Devuelve el nuevo pedido con un código de estado 200 OK
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"La mesa con el ID {idMesa} se cerró correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al intentar cerrar la mesa: {ex.Message}",
                    Data = null
                });
            }


        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpPost("AgregarEmpleado")]
        public async Task<ActionResult<EmpleadoCreateDto>> AgregarEmpleado([Required] string nombre, [Required] string usuario, [Required] string password, [Required] int sectorDelEmpleadoId, [Required] int rolDelEmpleadoId)
        {
            
            // Verifica que todos los parámetros sean válidos y no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) ||
                sectorDelEmpleadoId <= 0 || rolDelEmpleadoId <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Todos los campos son obligatorios. Nombre, usuario, contraseña deben ser válidos, y los IDs de sector y rol deben ser mayores que 0.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio para agregar empleado
                var resultado = await _socioServicio.AgregarEmpleado(nombre, usuario, password, sectorDelEmpleadoId, rolDelEmpleadoId);

                if (resultado == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se pudo agregar al empleado. Verifica que los datos proporcionados sean correctos.",
                        Data = null
                    });
                }
                // Devuelve un código de estado 200 OK con el empleado creado
                return Ok(new ApiResponse<EmpleadoCreateDto>
                {
                    Success = true,
                    Message = $"Empleado {nombre} agregado correctamente.",
                    Data = resultado
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al intentar agregar al empleado: {ex.Message}",
                    Data = null
                });
            }

        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpPut("SuspenderEmpleado/{idEmpleado}")]
        public async Task<ActionResult> SuspenderEmpleado(int idEmpleado)
        {
           
            // Verifica que id sea válido
            if (idEmpleado <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID del empleado debe ser un número válido mayor que 0.",
                    Data = null
                });
            }
            try
            {
                // Llama al servicio para suspender al empleado
                var resultado = await _socioServicio.SuspenderEmpleado(idEmpleado);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"El empleado con el ID {idEmpleado} no existe o ya se encuentra suspendido.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"El empleado con el ID {idEmpleado} fue suspendido correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error inesperado al intentar suspender al empleado: {ex.Message}",
                    Data = null
                });
            }


        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpDelete("BorrarEmpleado/{idEmpleado}")]
        public async Task<ActionResult> BorrarEmpleado(int idEmpleado)
        {
           
            // Verifica que id sea válido
            if (idEmpleado <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID del empleado debe ser mayor que 0.",
                    Data = null
                });
            }
            try
            {
                // Llama al servicio para borrar al empleado
                var resultado = await _socioServicio.BorrarEmpleado(idEmpleado);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"El empleado con el ID {idEmpleado} no existe o ya fue eliminado.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = $"El empleado con el ID {idEmpleado} fue eliminado correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al intentar eliminar al empleado: {ex.Message}",
                    Data = null
                });
            }

        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("CantidadEmpleadosPorSector")]
        public async Task<ActionResult<IEnumerable<EmpleadosPorSectorResponseDto>>> CantidadEmpleadosPorSector()
        {
           
            try
            {
                // Llama al servicio para obtener la cantidad de empleados
                var resultado = await _socioServicio.CantidadEmpleadosPorSector();

                // Si no hay empleados
                if (resultado == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No hay empleados para mostrar.",
                        Data = null
                    });
                }

                // Devuelve el el resultado con un código de estado 200 OK
                return Ok(new ApiResponse<IEnumerable<EmpleadosPorSectorResponseDto>>
                {
                    Success = true,
                    Message = "Cantidad de empleados por sector obtenida exitosamente.",
                    Data = resultado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener la cantidad de empleados por sector: {ex.Message}",
                    Data = null
                });
            }


        }

        //*MODIFICO ACA LA RESPUESTA INFORME - **** B
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("CantidadOperacionesPorSector/{idSector}")]
        public async Task<ActionResult<IEnumerable<OperacionesPorSectorDto>>> CantidadOperacionesPorSector(
             int idSector,
             [FromQuery] DateTime? fechaInicio,
             [FromQuery] DateTime? fechaFin)
        {
            // Verifica que el ID sea válido
            if (idSector <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID del sector debe ser mayor que 0.",
                    Data = null
                });
            }

            // Validar que fechaInicio no sea mayor que fechaFin
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio.Value > fechaFin.Value)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "La fecha de inicio no puede ser mayor que la fecha de fin.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio con los parámetros de fechas
                var resultado = await _socioServicio.CantidadOperacionesPorSector(idSector, fechaInicio, fechaFin);



                // Si no se encuentran operaciones
                if (resultado == null || !resultado.Any())
                {
                    string mensaje = fechaInicio.HasValue || fechaFin.HasValue
                        ? $"No se encontraron operaciones para el sector con ID: {idSector} dentro del rango de fechas especificado."
                        : $"No se encontraron operaciones para el sector con ID: {idSector}.";

                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = mensaje,
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<OperacionesPorSectorDto>>
                {
                    Success = true,
                    Message = "Operaciones obtenidas exitosamente por sector.",
                    Data = resultado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener la cantidad de operaciones por sector: {ex.Message}",
                    Data = null
                });
            }
        }


        //MODIFIQUE ACA INFORME C -cantidad de operaciones de todos por sector listada por cada empleado (c)
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("OperacionesDeTodosLosEmpleados")]
        public async Task<ActionResult<IEnumerable<OperacionesEmpleadoDto>>> ObtenerTodasLasOperacionesEmpleados(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Validación de parámetros
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "La fecha de inicio no puede ser posterior a la fecha de fin.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio pasándole las fechas de filtro
                var operaciones = await _socioServicio.ObtenerTodasLasOperacionesEmpleados(fechaInicio, fechaFin);

                // Si no se encontraron operaciones
                if (operaciones == null || !operaciones.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se encontraron operaciones para los filtros proporcionados.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<OperacionesEmpleadoDto>>
                {
                    Success = true,
                    Message = "Operaciones de empleados obtenidas exitosamente.",
                    Data = operaciones
                });

            }
            catch (Exception ex)
            {
                // Manejo de excepciones con detalles específicos
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener las operaciones de empleados: {ex.Message}",
                    Data = null
                });
            }

        }


        //MODIFIQUE ACA INFORME D-cantidad de operaciones de cada uno por separado (d)
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("OperacionesPorEmpleado/{idEmpleado}")]
        public async Task<ActionResult<IEnumerable<OperacionesEmpleadoDto>>> OperacionesPorEmpleado(int idEmpleado, DateTime? fechaInicio, DateTime? fechaFin)
        {

            // Verificar que el idEmpleado sea válido
            if (idEmpleado <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "El ID del empleado debe ser un número mayor que 0.",
                    Data = null
                });
            }

            // Validar que la fecha de inicio no sea posterior a la fecha de fin
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "La fecha de inicio no puede ser posterior a la fecha de fin.",
                    Data = null
                });
            }

            try
            {
                // Llama al servicio
                var operaciones = await _socioServicio.OperacionesPorEmpleado(idEmpleado, fechaInicio, fechaFin);

                if (operaciones == null || !operaciones.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"No se encontraron operaciones para el empleado con ID {idEmpleado}.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<OperacionesEmpleadoDto>>
                {
                    Success = true,
                    Message = "Operaciones obtenidas exitosamente.",
                    Data = operaciones
                });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones con respuesta estructurada
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener las operaciones del empleado: {ex.Message}",
                    Data = null
                });
            }
        }

        //MODIFIQUE ACA INFORME PEDIDOS C - LISTAR PEDIDOS CON DEMORA

        //[Authorize(Policy = "RequireMozoRole")]
        //[HttpGet("ListarPedidosConDemora")]
        //public async Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora(DateTime? fechaInicio, DateTime? fechaFin)
        //{
        //    // Validar que las fechas sean correctas
        //    if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
        //    {
        //        return BadRequest(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "La fecha de inicio no puede ser posterior a la fecha de fin.",
        //            Data = null
        //        });
        //    }

        //    try
        //    {
        //        // Llama al servicio para obtener el listado de pedidos con demora, pasando las fechas como parámetros
        //        var resultado = await _socioServicio.ListarPedidosConDemora(fechaInicio, fechaFin);

        //        // Si no hay resultados
        //        if (resultado == null || !resultado.Any())
        //        {
        //            return NotFound(new ApiResponse<object>
        //            {
        //                Success = false,
        //                Message = "No hay pedidos demorados para mostrar.",
        //                Data = null
        //            });
        //        }

        //        // Devuelve el resultado con un código de estado 200 OK
        //        return Ok(new ApiResponse<IEnumerable<PedidoDemoradoDto>>
        //        {
        //            Success = true,
        //            Message = "Pedidos demorados obtenidos exitosamente.",
        //            Data = resultado
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = $"Ocurrió un error al listar los pedidos con demora: {ex.Message}",
        //            Data = null
        //        });
        //    }

        //}
        
    }
}
