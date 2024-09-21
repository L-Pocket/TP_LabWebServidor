using Microsoft.AspNetCore.Mvc;
using Restaurante_API.Servicios;

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

        [HttpGet]
        public async Task<IActionResult> Getlogs()
        {
            var logs = await _LogEmpleadoServicio.GetLog();
            return Ok(logs);
        }
    }
}
