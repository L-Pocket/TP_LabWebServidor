using Restaurante_API.Servicios;
using System.Diagnostics;

namespace Restaurante_API.Middleware
{
    public class LogMiddleware
    {
        private readonly ILogger<LogMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(ILogger<LogMiddleware> logger, RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var logueoEmpleadoServicio = context.RequestServices.GetRequiredService<ILogEmpleadoServicio>();

            int empleadoId = ObtenerEmpleadoId(context);

            if (empleadoId != 0)
            {

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                await logueoEmpleadoServicio.RegistrarLogueo(empleadoId);
                this._logger.LogInformation($"Empleado {empleadoId} ingreso al sistema{DateTime.Now}");
                try { await _next(context); }
                finally
                {
                    stopwatch.Stop();

                    await logueoEmpleadoServicio.RegistrarDeslogueo(empleadoId);
                    this._logger.LogInformation($"El empleado {empleadoId} salio del sistema:{DateTime.Now}");
                    this._logger.LogInformation($"Tiempo de sesión del empleado {empleadoId}: {stopwatch.Elapsed}");
                }


            }
            else
            {
                await _next(context); // Continuar con el siguiente middleware si no hay empleadoId
            }


        }
        private int ObtenerEmpleadoId(HttpContext context)
        {
           
            var empleadoId = context.Session.GetInt32("EmpleadoId") ?? 0;
            Console.WriteLine($"EmpleadoId obtenido de la sesión en el middleware: {empleadoId}");

            return empleadoId;
        }

    }
}
