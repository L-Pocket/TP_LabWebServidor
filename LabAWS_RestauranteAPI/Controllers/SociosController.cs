using LabAWS_RestauranteAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController: ControllerBase
    {
        private readonly RestauranteContext _context;

        // Constructor
        public SociosController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpPost("AgregarEmpleado")]
        public async Task<ActionResult<Empleado>> AgregarEmpleado([FromQuery] string nombre, [FromQuery] string usuario, [FromQuery] string password,
            [FromQuery] int sectorDelEmpleadoId, [FromQuery] int rolDelEmpleadoId)
        {
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
                    IdSector = sectorDelEmpleadoId,
                    IdRol = rolDelEmpleadoId,
                    //EmpleadoActivo = true // Por defecto, el empleado está activo al momento de la creación
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
    }
}
