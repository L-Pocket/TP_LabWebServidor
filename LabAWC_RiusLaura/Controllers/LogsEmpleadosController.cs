using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante_API.Servicios;
using System.Security.Claims;

namespace Restaurante_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsEmpleadosController : ControllerBase
    {
        private readonly ILogEmpleadoServicio _LogEmpleadoServicio;

        public LogsEmpleadosController(ILogEmpleadoServicio LogEmpleadoServicio)
        {
            this._LogEmpleadoServicio = LogEmpleadoServicio;
        }

        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("logs")]
        // INFORME A!!!
        public async Task<IActionResult> GetLogs([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
        {
            // Validar que la fecha de inicio no sea mayor que la fecha de fin
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio.Value > fechaFin.Value)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "La fecha de inicio no puede ser mayor que la fecha de fin."
                });
            }

            // Llamar al servicio para obtener los logs
            var logs = await _LogEmpleadoServicio.GetLog(fechaInicio, fechaFin);

            // Si no se encuentran logs, retornar mensaje
            if (logs == null || !logs.Any())
            {
                return NotFound(new
                {
                    Success = false,
                    Message = "No se encontraron registros para el rango de fechas especificado."
                });
            }

            // Retornar los logs si existen
            return Ok(new
            {
                Success = true,
                Message = "Registros obtenidos correctamente.",
                Data = logs
            });

        }
    }
}
