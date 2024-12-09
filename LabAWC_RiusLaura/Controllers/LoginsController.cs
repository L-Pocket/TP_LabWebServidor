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
       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            try
            {
                // Obtenemos el empleadoId y el rol del usuario
                var empleado = await _logEmpleadoServicio.IniciarSesion(login.usuario, login.password);

                if (empleado == null)
                {
                    return Unauthorized("Credenciales incorrectas.");
                }

                var rol = empleado.Rol.Descripcion;  // Rol del empleado
                var sectorId = empleado.SectorId;    // Obtenemos el sectorId desde el empleado

                // Generamos el token, ahora incluyendo el sectorId
                var token = _authServicio.CreateToken(login, rol, empleado.Id, sectorId);

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /*
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {

            try
            {
                var empleadoId = await _logEmpleadoServicio.IniciarSesion(login.usuario, login.password);
                var rol = empleadoId.Rol.Descripcion;
                var token = _authServicio.CreateToken(login, rol, empleadoId.Id);

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }

        */
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Obtengo el empleadoId del token JWT
                var empleadoIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "EmpleadoId");
                if (empleadoIdClaim == null)
                {
                    return Unauthorized("No se pudo obtener el ID del empleado del token.");
                }

                int empleadoId = int.Parse(empleadoIdClaim.Value);

                // Llama a registrar el deslogueo
                await _logEmpleadoServicio.RegistrarDeslogueo(empleadoId);

                return Ok(new { mensaje = "Deslogueo exitoso." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar desloguear.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al desloguear.");
            }
        }
    }
}
