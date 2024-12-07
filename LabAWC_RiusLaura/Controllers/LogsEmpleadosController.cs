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

            var logs = await _LogEmpleadoServicio.GetLog(fechaInicio, fechaFin);
            return Ok(logs);

        }
    }
}
