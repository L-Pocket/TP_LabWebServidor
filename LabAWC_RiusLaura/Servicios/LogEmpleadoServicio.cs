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

        public Task<List<EmpleadosLogDto>> GetLog();

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

        public async Task<List<EmpleadosLogDto>> GetLog()
        {
            var logs = await _context.LogueosEmpleados.ToListAsync();
            //mapeo
            var logsResponseDto = this._mapper.Map<List<EmpleadosLogDto>>(logs);
            return logsResponseDto;
        }
    }
}
