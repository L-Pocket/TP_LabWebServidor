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
                return BadRequest("IdMesa debe ser válido.");
            }
            try
            {
                // Llama al servicio para modificar mesa
                var resultado = await _socioServicio.CerrarMesa(idMesa);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound($"La mesa con el id {idMesa} no existe o no está en un estado válido para cerrar.");
                }

                // Devuelve el nuevo pedido con un código de estado 200 OK
                return Ok($"La mesa con el id {idMesa} se cerró correctamente");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al intentar cerrar la mesa: {ex.Message}");
            }


        }        

        [HttpPost("AgregarEmpleado")]
        public async Task<ActionResult<EmpleadoCreateDto>> AgregarEmpleado([Required] string nombre, [Required] string usuario, [Required] string password, [Required] int sectorDelEmpleadoId, [Required] int rolDelEmpleadoId)
        {
            // Verifica que todos los parámetros sean válidos y no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) ||
                sectorDelEmpleadoId <= 0 || rolDelEmpleadoId <= 0)
            {
                return BadRequest("Nombre, usuario, contraseña, sectorDelEmpleadoId y rolDelEmpleadoId son obligatorios y deben ser válidos.");
            }

            try
            {
                // Llama al servicio para agregar empleado
                var resultado = await _socioServicio.AgregarEmpleado(nombre, usuario, password, sectorDelEmpleadoId, rolDelEmpleadoId);

                if (resultado == null)
                {
                    return NotFound("No se agregó empleado ya que alguno de los datos son incorrectos.");
                }
                // Devuelve un código de estado 200 OK con el empleado creado
                return Ok($"Se agregó a {nombre} como empleado.");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocurrió un error al agregar empleado: {ex.Message}");
            }

        }

        [HttpPut("SuspenderEmpleado/{idEmpleado}")]
        public async Task<ActionResult> SuspenderEmpleado(int idEmpleado)
        {
            // Verifica que id sea válido
            if (idEmpleado <= 0)
            {
                return BadRequest("IdEmpleado debe ser válido.");
            }
            try
            {
                // Llama al servicio
                var resultado = await _socioServicio.SuspenderEmpleado(idEmpleado);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound($"El empleado con el id {idEmpleado} no existe o ya está suspendido.");
                }

                return Ok($"SE SUSPENDIÓ CORRECTAMENTE al empleado con el id {idEmpleado}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al intentar suspender empleado: {ex.Message}");
            }


        }

        [HttpDelete("BorrarEmpleado/{idEmpleado}")]
        public async Task<ActionResult> BorrarEmpleado(int idEmpleado)
        {
            // Verifica que id sea válido
            if (idEmpleado <= 0)
            {
                return BadRequest("IdEmpleado debe ser válido.");
            }
            try
            {
                // Llama al servicio
                var resultado = await _socioServicio.BorrarEmpleado(idEmpleado);

                if (!resultado) // si el resultado es = false
                {
                    return NotFound($"El empleado con el id {idEmpleado} no existe.");
                }

                return Ok($"SE ELIMINÓ CORRECTAMENTE al empleado con el id {idEmpleado}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al intentar eliminar empleado: {ex.Message}");
            }

        }

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

                    return NotFound("No hay empleados para mostrar");
                }

                // Devuelve el el resultado con un código de estado 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener el producto más vendido: {ex.Message}");
            }


        }

        [HttpGet("CantidadOperacionesPorSector/{idSector}")]
        public async Task<ActionResult<IEnumerable<OperacionesPorSectorDto>>> CantidadOperacionesPorSector(int idSector)
        {
            // Verifica que id sea válido
            if (idSector <= 0)
            {
                return BadRequest("IdSector debe ser válido.");
            }
            try
            {
                var resultado = await _socioServicio.CantidadOperacionesPorSector(idSector);

                if (resultado == null || !resultado.Any())
                {
                    return NotFound($"No se encontraron operaciones para el sector con ID: {idSector}");
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener la cantidad de operaciones por sector: {ex.Message}");
            }
        }


        //cantidad de operaciones de todos por sector listada por cada empleado (c)

        [HttpGet("OperacionesDeTodosLosEmpleados")]
        public async Task<ActionResult<IEnumerable<OperacionesEmpleadoDto>>> ObtenerTodasLasOperacionesEmpleados()
        {
            var operaciones = await _socioServicio.ObtenerTodasLasOperacionesEmpleados();

            if (operaciones == null || !operaciones.Any())
            {
                return NotFound("No se encontraron operaciones.");
            }

            return Ok(operaciones);
        }


        //cantidad de operaciones de cada uno por separado (d)

        [HttpGet("OperacionesPorEmpleado/{idEmpleado}")]
        public async Task<ActionResult<IEnumerable<OperacionesEmpleadoDto>>> OperacionesPorEmpleado(int idEmpleado)
        {
            var operaciones = await _socioServicio.OperacionesPorEmpleado(idEmpleado);

            if (operaciones == null || !operaciones.Any())
            {
                return NotFound($"Empleado con Id {idEmpleado} no encontrado.");
            }

            return Ok(operaciones);
        }



        [HttpGet("ListarPedidosConDemora")]
        public async Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora()
        {
            try
            {
                // Llama al servicio para obtener el listado
                var resultado = await _socioServicio.ListarPedidosConDemora();

                // Si no hay listado
                if (resultado == null)
                {
                    return NotFound("No pedidos con demora para mostrar");
                }
                // Devuelve el el resultado con un código de estado 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al listar los productos con demora: {ex.Message}");
            }
            
        }
        
    }
}
