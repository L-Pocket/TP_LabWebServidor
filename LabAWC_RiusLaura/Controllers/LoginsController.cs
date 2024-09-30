using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante_API.DTO;
using Restaurante_API.Servicios;
using System.ComponentModel.DataAnnotations;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
       

        private readonly ILogEmpleadoServicio _logEmpleadoServicio;
        private readonly AuthServicio _authServicio;
        private readonly ILogger<LoginsController> _logger;
        public  LoginsController(ILogEmpleadoServicio logEmpleadoServicio, AuthServicio authServicio, ILogger<LoginsController> logger)
        {
            this._logEmpleadoServicio = logEmpleadoServicio;
            this._authServicio = authServicio;
            this._logger = logger;
        }
        // codigo viejo
        /*[HttpPost]

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
        }*/
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {

            try
            {
                var empleadoId = await _logEmpleadoServicio.IniciarSesion(login.usuario, login.password);
                HttpContext.Session.SetInt32("EmpleadoId", empleadoId.RolDelEmpleadoId);// Guarda el empleadoId en la sesión
                var GuardaEmpleadoId = HttpContext.Session.GetInt32("EmpleadoId");
                
                var rol = empleadoId.RolDelEmpleado.DescripcionRol == "Socio" ? "Socio" : "Empleado";
                var token = _authServicio.CreateToken(login, rol);

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }
    }
}
