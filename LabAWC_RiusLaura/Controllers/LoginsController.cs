using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante_API.Servicios;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        // Logins en el Endpoint.
        // Se maneja con JWT

        private readonly ILogEmpleadoServicio _logEmpleadoServicio;

        public  LoginsController(ILogEmpleadoServicio logEmpleadoServicio)
        {
            _logEmpleadoServicio = logEmpleadoServicio;
        }

        [HttpPost]

        public async Task<IActionResult> Login( string userName, string password)
        {
            try{ 
            
                var empleadoId = await _logEmpleadoServicio.IniciarSesion(userName, password);
                // Guarda el empleadoId en la sesión
                HttpContext.Session.SetInt32("EmpleadoId", empleadoId);
                var GuardaEmpleadoId = HttpContext.Session.GetInt32("EmpleadoId");
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}
