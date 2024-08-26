using LabAWS_RestauranteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //agrego este entity para que funcione

namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {

        // Login de empleado.

        private readonly RestauranteContext _context;
        private static List<Logueo> _logins = new List<Logueo>();

        public LoginsController(RestauranteContext context)
        {
            _context = context;
        }



        [HttpGet("dias-horarios-logueos")]
        public async Task<IActionResult> GetDiasHorariosLogueos()
        {
            var logueos = await _context.Logueos
                .Where(l => l.IdEmpleado.HasValue)  // nos aseguramos que el logueo este asociado a un empleado
                .Select(l => new
                {
                    EmpleadoId = l.IdEmpleado,                   // ID del empleado
                    NombreEmpleado = l.IdEmpleadoNavigation.Nombre, // Nombre del empleado
                    FechaLogueo = l.FechaLogueo,                  // Fecha y hora de logueo
                    Dia = l.FechaLogueo.DayOfWeek,                // Día de la semana en que el empleado se logueo
                    Hora = l.FechaLogueo.TimeOfDay                // Hora del logueo
                })
                .ToListAsync();

            if (!logueos.Any())
            {
                return NotFound("No fueron encontrados logueos");
            }

            return Ok(logueos);
        }


        //otra forma de buscar un logeo de usuario por el nombre del usuario

        [HttpGet("nombreEmpleado -dias-horarios-logueos")]
        public async Task<IActionResult> GetDiasHorariosLogueos([FromQuery] string nombreEmpleado)
        {
            var logueosQuery = _context.Logueos
                .Where(l => l.IdEmpleado.HasValue)  // nos aseguramos que el logueo este asociado a un empleado
                .Select(l => new
                {
                    EmpleadoId = l.IdEmpleado,
                    NombreEmpleado = l.IdEmpleadoNavigation.Nombre,
                    FechaLogueo = l.FechaLogueo,
                    Dia = l.FechaLogueo.DayOfWeek,
                    Hora = l.FechaLogueo.TimeOfDay
                });

            // en caso que queramos que el dueño quiera filtrar por el nombre del empleado
            if (!string.IsNullOrEmpty(nombreEmpleado))
            {
                logueosQuery = logueosQuery.Where(l => l.NombreEmpleado.Contains(nombreEmpleado));
            }

            var logueos = await logueosQuery.ToListAsync();

            if (!logueos.Any())
            {
                return NotFound("No fueron encontrados logueos");
            }

            return Ok(logueos);
        }



    }
}
