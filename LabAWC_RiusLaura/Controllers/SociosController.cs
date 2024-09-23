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

        [HttpPut("CerrarMesa/{idMesa}")]
        public async Task<ActionResult<MesaDto>> CerrarMesa(int idMesa)
        {
            // Verifica que id mesa sea válido
            if (idMesa <= 0)
            {
                return BadRequest("IdMesa debe ser válido.");
            }
            try
            {
                // Llama al servicio para modificar mesa
                var resultado = await _socioServicio.CerrarMesa(idMesa);

                if (resultado == null)
                {
                    return NotFound($"La mesa con el id {idMesa} no existe o no está en un estado válido para cerrar.");
                }

                // Devuelve el nuevo pedido con un código de estado 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al intentar cerrar la mesa: {ex.Message}");
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

                    return NotFound("No se encontró ningún empleado");
                }

                // Devuelve el el resultado con un código de estado 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener el producto más vendido: {ex.Message}");
            }


        }

        [HttpPost("AgregarEmpleado")]
        public async Task<ActionResult<EmpleadoCreateDto>> AgregarEmpleado([FromQuery] string nombre, [FromQuery] string usuario, [FromQuery] string password,
            [FromQuery] int sectorDelEmpleadoId, [FromQuery] int rolDelEmpleadoId)
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
                    return NotFound("No se agregó empleado");
                }
                // Devuelve un código de estado 200 OK con el empleado creado
                return Ok(resultado);

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

                if (resultado == null)
                {
                    return NotFound($"El empleado con el id {idEmpleado} no existe o ya se encuentra suspendido.");
                }

                return Ok(resultado);
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

                if (resultado == null)
                {
                    return NotFound($"El empleado con el id {idEmpleado} no existe.");
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al intentar eliminar empleado: {ex.Message}");
            }

        }

        //[HttpGet("ListarPedidosConDemora")]
        //public async Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora()
        //{
        //    var result = await _socioServicio.ListarPedidosConDemora();
        //    return result;
        //}

        //[HttpGet("ListarLogueosEmpleados")]
        //public async Task<ActionResult<IEnumerable<LogueoEmpleadoDto>>> ListarLogueosEmpleados()
        //{
        //    var result = await _socioServicio.ListarLogueosEmpleados();
        //    return result;
        //}
    }
}




/*
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private readonly DataContext _context;

        // Constructor
        public SociosController(DataContext context)
        {
            _context = context;
        }

        // PUT // Alguno de los socios cierra la mesa.
        // Descripción: apunta a EstadosMesa, cambia a "Cerrada".
        [HttpPut("CerrarMesa/{idMesa}")]
        public async Task<ActionResult<Mesa>> CerrarMesa(int idMesa)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(idMesa); // Busca la mesa en BBDD
                if (mesa == null)
                    return NotFound($"No se encontró una mesa con el id {idMesa}.");

                if (mesa.EstadoDeMesaId == 3) // 3 = "Cliente pagando"
                {
                    mesa.EstadoDeMesaId = 4; // 4 = "Cerrada"
                    await _context.SaveChangesAsync();

                    return Ok(mesa);
                }
                else
                {
                    return BadRequest($"La mesa con el id {idMesa} no está en un estado válido para cerrar.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET // Alguno de los socios pide el listado de pedidos y el tiempo de demora de ese pedido.
        [HttpGet("ListarPedidosConDemora")]
        public async Task<ActionResult<IEnumerable<object>>> ListarPedidosConDemora()
        {
            try
            {
                var pedidos = await _context.Pedidos // Busca Pedidos en la BBDD
                    .Where(p => p.EstadoDelPedidoId == 1) // Filtrar por pedidos pendientes
                    .Select(p => new  //nuevo objeto con los siguientes campos:
                    {
                        p.IdPedido,
                        p.ComandaDelPedidoId,
                        p.TiempoEstimado,
                        TiempoReal = EF.Functions.DateDiffMinute(p.FechaCreacion, DateTime.Now), // Calcular la diferencia en minutos
                        Estado = p.EstadoDelPedido.DescripcionPedido
                    })
                    .Where(p => p.TiempoEstimado < p.TiempoReal) // Filtrar aquellos que están demorados
                    .ToListAsync();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // GET
        // Alguno de los socios pide el listado de las mesas y sus estados.
        [HttpGet("ListarMesasConEstados")]
        public async Task<ActionResult<IEnumerable<object>>> ListarMesasConEstados()
        {
            try
            {
                var mesas = await _context.Mesas // Busca las mesas en BBDD
                    .Select(m => new
                    {
                        m.IdMesa,
                        m.CodigoMesa,
                        Estado = m.EstadoDeMesa.DescripcionMesa
                    })
                    .ToListAsync();

                if (mesas == null || mesas.Count == 0) // Validar si no hay mesas registradas
                {
                    return NotFound("No se encontraron mesas registradas.");
                }

                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // ----- Requerimientos de la aplicación: -----

        // GET //Los días y horarios que se Ingresaron al sistema los empleados.
        [HttpGet("CantidadEmpleadosPorSector")]
        public async Task<ActionResult<IEnumerable<object>>> CantidadEmpleadosPorSector()
        {
            try
            {
                var resultado = await _context.Empleados // Busca los Empleados en la BBDD
                    .GroupBy(e => e.SectorDelEmpleadoId) // agrupar según sector
                    .Select(g => new //Crea un objeto anónimo con los atributos:
                    {
                        Sector = g.FirstOrDefault().SectorDelEmpleado.DescripcionSector, // se usa para acceder al primer empleado del grupo y obtener su sector
                        CantidadEmpleados = g.Count() // La cantidad de empleados en cada grupo
                    })
                    .ToListAsync();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST //Posibilidad de dar de alta a nuevos empleados.
        [HttpPost("AgregarEmpleado")]
        public async Task<ActionResult<Empleado>> AgregarEmpleado([FromQuery] string nombre, [FromQuery] string usuario, [FromQuery] string password,
            [FromQuery] int sectorDelEmpleadoId, [FromQuery] int rolDelEmpleadoId)
        {
            // Verifica que todos los parámetros sean válidos y no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) ||
                sectorDelEmpleadoId <= 0 || rolDelEmpleadoId <= 0)
            {
                return BadRequest("Nombre, usuario, contraseña, sectorDelEmpleadoId y rolDelEmpleadoId son obligatorios y deben ser válidos.");
            }

            try
            {
                // Crear una nueva instancia de Empleado con los parámetros proporcionados
                var nuevoEmpleado = new Empleado
                {
                    Nombre = nombre,
                    Usuario = usuario,
                    Password = password,
                    SectorDelEmpleadoId = sectorDelEmpleadoId,
                    RolDelEmpleadoId = rolDelEmpleadoId,
                    EmpleadoActivo = true // Por defecto, el empleado está activo al momento de la creación
                };

                // Añadir el nuevo empleado a la base de datos
                _context.Empleados.Add(nuevoEmpleado);
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                // Devuelve un código de estado 200 OK con el empleado creado
                return Ok(nuevoEmpleado);
            }
            catch (Exception ex)
            {
                // Manejo de errores, devolver un código de estado 500 Internal Server Error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT //Posibilidad de suspender empleados
        // Crear OTRO ATRIBUTO EmpleadoActivo tipo bool
        [HttpPut("SuspenderEmpleado/{idEmpleado}")]
        public async Task<ActionResult<Empleado>> SuspenderEmpleado(int idEmpleado)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(idEmpleado); // Busca el empleado por su ID
                if (empleado == null)
                {
                    return NotFound($"No se encontró un empleado con el id {idEmpleado}.");
                }

                if (empleado.EmpleadoActivo == false) // Valida si el empleado ya está suspendido
                {
                    return BadRequest("El empleado ya se encuentra suspendido.");
                }

                empleado.EmpleadoActivo = false; // Suspender el empleado
                await _context.SaveChangesAsync(); // Guardar cambios

                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        //DELETE //Posibilidad de borrar empleados.
        [HttpDelete("BorrarEmpleado/{idEmpleado}")]
        public async Task<ActionResult> BorrarEmpleado(int idEmpleado)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(idEmpleado); // Busca el empleado por su ID
                if (empleado == null)
                {
                    return NotFound($"No se encontró un empleado con el id {idEmpleado}.");
                }

                _context.Empleados.Remove(empleado); // Remover de la BBDD
                await _context.SaveChangesAsync(); // Guardar cambios

                return Ok($"Se eliminó al empleado con el id {idEmpleado}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // FALTA-------------------------------------------------------------

        // GET
        //Cantidad de operaciones de todos por sector, listada por cada empleado.
        // Descripción: listar productos por empleado.

        
        [HttpGet("ListarLogueosEmpleados")]
        public async Task<ActionResult<IEnumerable<object>>> ListarLogueosEmpleados()
        {
            try
            {
                var logueos = await _context.LogueosEmpleados // Busca los logueos en la BBDD
                    .Select(l => new
                    {
                        l.EmpleadoLogId,
                        l.EmpleadoLog.Nombre, // Obtener el nombre del empleado
                        FechaLogueo = l.FechaLogueo.ToString("yyyy-MM-dd HH:mm:ss"), // Convierte a string ya que es un tipo DateTime
                        // Operador condicional, ? si HasValue es True convierte a string, : si es false devuelve null como tipo string
                        FechaDeslogueo = l.FechaDeslogueo.HasValue ? l.FechaDeslogueo.Value.ToString("yyyy-MM-dd HH:mm:ss") : (string)null
                    })
                    .ToListAsync();

                if (!logueos.Any())
                {
                    return NotFound("No hay registros de logueo de empleados.");
                }

                return Ok(logueos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // GET
        //Cantidad de operaciones de todos por sector.
        //Descripción: listar empleados por sector. 

    }
}
*/