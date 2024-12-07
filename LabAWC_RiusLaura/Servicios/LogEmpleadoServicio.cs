using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;
using Restaurante_API.DTO;

namespace Restaurante_API.Servicios
{
    public interface ILogEmpleadoServicio
    {
        public Task<Empleado> IniciarSesion(string usuario, string password);
        public Task RegistrarLogueo(int empleadoId);
        public Task RegistrarDeslogueo(int empleadoId);

        public Task<List<EmpleadosLogDto>> GetLog(DateTime? fechaInicio, DateTime? fechaFin);

    }
    public class LogEmpleadoServicio : ILogEmpleadoServicio
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public LogEmpleadoServicio(DataContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        public async Task<Empleado> IniciarSesion(string usuario, string password)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Rol)  // Incluye la tabla de roles
                .FirstOrDefaultAsync(e => e.Usuario == usuario && e.Password == password);

            if (empleado == null)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }
            RegistrarLogueo(empleado.Id);
            return empleado;
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
            var log = await _context.LogueosEmpleados
                .Where(l => l.EmpleadoLogId == empleadoId && l.FechaDeslogueo==null)
                .OrderByDescending(l => l.FechaLogueo)
                .FirstOrDefaultAsync();

            if (log != null)
            {
                log.FechaDeslogueo = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        //INFORME A - Los días y horarios que se Ingresaron al sistema.
        public async Task<List<EmpleadosLogDto>> GetLog(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.LogueosEmpleados
                                .Include(log => log.EmpleadoLog)  // Incluimos la relación con Empleados
                                .AsQueryable();

            if (fechaInicio.HasValue && !fechaFin.HasValue)
            {
                query = query.Where(log => log.FechaLogueo.Date == fechaInicio.Value.Date);
            }

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(log => log.FechaLogueo.Date >= fechaInicio.Value.Date
                                           && log.FechaLogueo.Date <= fechaFin.Value.Date);
            }

            // para devolver los datos en el formato que queremos
            return await query.Select(log => new EmpleadosLogDto
            {
                //id = log.Id, 
                fechaLogueo = log.FechaLogueo,
                fechaDeslogueo = log.FechaDeslogueo,
                empleadoLogId = log.EmpleadoLogId,
                EmpleadoNombre = log.EmpleadoLog.Nombre  // Accedemos al nombre del empleado
            }).ToListAsync();
        }
    }
}
