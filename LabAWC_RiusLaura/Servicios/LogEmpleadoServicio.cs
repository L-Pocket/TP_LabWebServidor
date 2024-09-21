using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Restaurante_API.Servicios
{
    public interface ILogEmpleadoServicio
    {
        public Task<int> IniciarSesion(string usuario, string password);
        public Task RegistrarLogueo(int empleadoId);
        public Task RegistrarDeslogueo(int empleadoId);

        public Task<List<LogueoEmpleado>> GetLog();

    }
    public class LogEmpleadoServicio : ILogEmpleadoServicio
    {
        private readonly DataContext _context;

        public LogEmpleadoServicio(DataContext context)
        {
            _context = context;
        }

        public async Task<int> IniciarSesion(string usuario, string password)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Usuario == usuario && e.Password == password);

            if (empleado == null)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }
            return empleado.IdEmpleado;
        }
        public async Task RegistrarLogueo(int empleadoId)
        {
           var empleado = await _context.Empleados.FindAsync(empleadoId);
           if (empleado == null)
            {
                throw new Exception("El empleado no existe");
            }
            var log = new LogueoEmpleado
            {
                EmpleadoLogId = empleadoId,
                FechaLogueo = DateTime.Now
            };
            _context.LogueosEmpleados.Add(log);// Agrega el registro de logueo a la base de datos
             await _context.SaveChangesAsync(); // Guarda los cambios

        }
        public async Task RegistrarDeslogueo(int empleadoId)
        {
            var log = await _context.LogueosEmpleados.
                Where(l => l.EmpleadoLogId == empleadoId && l.FechaDeslogueo==null).
                OrderByDescending(l =>l.FechaDeslogueo).
                FirstOrDefaultAsync();

            if (log != null)
            {
                log.FechaDeslogueo = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LogueoEmpleado>> GetLog()
        {
            var logs = await _context.LogueosEmpleados.ToListAsync();
            return logs;
        }
    }
}
