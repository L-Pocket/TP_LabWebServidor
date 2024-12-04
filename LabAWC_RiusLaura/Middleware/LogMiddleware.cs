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

            this._logger.LogInformation("Antes de ejecutar en endpoint");
            await _next(context);
            this._logger.LogInformation("Despues de ejecutar en endpoint");

        }
        

    }
}
