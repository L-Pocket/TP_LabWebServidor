using LabAWS_RiusLaura.DTO;
using LabAWC_RiusLaura.DAL.Data;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Restaurante_API.DTO;

namespace LabAWS_RiusLaura.Servicios
{
    public interface ISocioServicio
    {
        Task<ActionResult<MesaDto>> CerrarMesa(int idMesa);
        Task<ActionResult<IEnumerable<EmpleadosPorSectorResponseDto>>> CantidadEmpleadosPorSector();
        Task<ActionResult<EmpleadoCreateDto>> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId);
        Task<ActionResult<bool>> SuspenderEmpleado(int idEmpleado);
        Task<ActionResult<bool>> BorrarEmpleado(int idEmpleado);

        //Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora();      
        //Task<ActionResult<IEnumerable<LogueoEmpleadoDto>>> ListarLogueosEmpleados();
    }

    public class SocioServicio : ISocioServicio
    {
        private readonly DataContext _context;
        private readonly ILogger<PedidoServicio> logger;
        private readonly IMapper mapper;

        public SocioServicio(DataContext context, ILogger<PedidoServicio> logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ActionResult<MesaDto>> CerrarMesa(int idMesa)
        {
            this.logger.LogInformation("Iniciando cerrar mesa.");
            // Buscar la mesa en la BBDD por su ID
            Mesa mesaEnt = await _context.Mesas.FindAsync(idMesa);
            if (mesaEnt == null)
            {
                this.logger.LogWarning($"No se encontró la mesa con ID: {idMesa}");
                return null;
            }

            // Verificar el estado de la mesa
            if (mesaEnt.EstadoDeMesaId == 3) // 3 = "Cliente pagando"
            {
                mesaEnt.EstadoDeMesaId = 4; // 4 = "Cerrada"
                await _context.SaveChangesAsync();
                this.logger.LogInformation("Mesa modificada exitosamente.");

                // Mapear Mesa a MesaDto para devolverlo al controller
                var mesaDto = this.mapper.Map<MesaDto>(mesaEnt);

                // Retorna
                return mesaDto;

            }
            else
            {
                this.logger.LogWarning($"La mesa con el id {idMesa} no está en un estado válido para cerrar.");
                return null;
            }

        }

        public async Task<ActionResult<IEnumerable<EmpleadosPorSectorResponseDto>>> CantidadEmpleadosPorSector()
        {
            this.logger.LogInformation("Iniciando la búsqueda del empleados por sector.");

            // Agrupa los empleados por el sectorID y calcula la cantidad total por sector
            var listado = await _context.Empleados
                .GroupBy(e => e.SectorDelEmpleadoId)
                .Select(g => new EmpleadosPorSectorResponseDto // creamos un nuevo objeto anónimo con dos propiedades elsector y la cantidad 
                {
                    Sector = g.FirstOrDefault().SectorDelEmpleado.DescripcionSector,
                    CantidadEmpleados = g.Count()
                })
                .ToListAsync();

            // Si no se encuentra devuelve un mensaje de error
            if (listado == null)
            {
                this.logger.LogWarning("No se encontró ningún empleado.");
                return null;
            }

            this.logger.LogInformation("Busqueda finalizada con exito.");
            return listado;

        }

        public async Task<ActionResult<EmpleadoCreateDto>> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId)
        {
            this.logger.LogInformation("Iniciando Agregar Empleado.");

            // Verificar si el sector existe
            var sectorExistente = await _context.Sectores.FindAsync(sectorDelEmpleadoId);
            if (sectorExistente == null)
            {
                this.logger.LogWarning($"Sector no encontrado con ID: {sectorDelEmpleadoId}");
                return null; // Si el sector no existe, retorna null
            }

            // Verificar si el rol existe
            var rolExistente = await _context.Roles.FindAsync(rolDelEmpleadoId);
            if (rolExistente == null)
            {
                this.logger.LogWarning($"Rol no encontrado con ID: {rolDelEmpleadoId}");
                return null; // Si el rol no existe, retorna null
            }

            // Creación del nuevo empleado Mapeando DTO a entidad Empleado            
            var nuevoEmpleado = this.mapper.Map<Empleado>(new EmpleadoCreateDto
            {
                Nombre = nombre,
                Usuario = usuario,
                EmpleadoActivo = true // El empleado siempre se crea como activo
            });
            nuevoEmpleado.SectorDelEmpleadoId = sectorDelEmpleadoId;
            nuevoEmpleado.RolDelEmpleadoId = rolDelEmpleadoId;
            nuevoEmpleado.Password = password;

            // Agregar el nuevo empleado a la BBDD
            _context.Empleados.Add(nuevoEmpleado);
            await _context.SaveChangesAsync();
            this.logger.LogInformation($"Empleado creado exitosamente con ID: {nuevoEmpleado.IdEmpleado}");

            // Mapear la entidad Empleado a empleadoDTO para devolverlo al controller
            var empleadoDto = this.mapper.Map<EmpleadoCreateDto>(nuevoEmpleado);

            // Retorna
            return empleadoDto;

        }

        public async Task<ActionResult<bool>> SuspenderEmpleado(int idEmpleado)
        {
            this.logger.LogInformation("Iniciando Suspender Empleado.");

            // Buscar al empleado en la BBDD por su ID
            var empleado = await _context.Empleados.FindAsync(idEmpleado);

            if (empleado == null)
            {
                this.logger.LogWarning($"No se encontró el empleado con ID: {idEmpleado}");
                return null;
            }

            if (empleado.EmpleadoActivo == false)
            {
                this.logger.LogWarning($"El empleado con ID: {idEmpleado} ya se encuentra suspendido.");
                return null;
            }

            empleado.EmpleadoActivo = false;
            await _context.SaveChangesAsync();
            this.logger.LogInformation("Empleado suspendido exitosamente.");

            return true;

        }

        public async Task<ActionResult<bool>> BorrarEmpleado(int idEmpleado)
        {
            this.logger.LogInformation("Iniciando Eliminar Empleado.");

            // Buscar al empleado en la BBDD por su ID
            var empleado = await _context.Empleados.FindAsync(idEmpleado);

            if (empleado == null)
            {
                this.logger.LogWarning($"No se encontró el empleado con ID: {idEmpleado}");
                return null;
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            this.logger.LogInformation("Empleado eliminado exitosamente.");

            return true;

        }

        //public async Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora()
        //{
        //    try
        //    {
        //        var pedidos = await _context.Pedidos
        //            .Where(p => p.EstadoDelPedidoId == 1)
        //            .Select(p => new PedidoDemoradoDto
        //            {
        //                IdPedido = p.IdPedido,
        //                ComandaDelPedidoId = p.ComandaDelPedidoId,
        //                TiempoEstimado = p.TiempoEstimado,
        //                TiempoReal = EF.Functions.DateDiffMinute(p.FechaCreacion, DateTime.Now),
        //                Estado = p.EstadoDelPedido.DescripcionPedido
        //            })
        //            .Where(p => p.TiempoEstimado < p.TiempoReal)
        //            .ToListAsync();

        //        return new OkObjectResult(pedidos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
        //    }
        //}


        //public async Task<ActionResult<IEnumerable<LogueoEmpleadoDto>>> ListarLogueosEmpleados()
        //{
        //    try
        //    {
        //        var logueos = await _context.LogueosEmpleados
        //            .Select(l => new LogueoEmpleadoDto
        //            {
        //                EmpleadoLogId = l.EmpleadoLogId,
        //                Nombre = l.EmpleadoLog.Nombre,
        //                FechaLogueo = l.FechaLogueo.ToString("yyyy-MM-dd HH:mm:ss"),
        //                FechaDeslogueo = l.FechaDeslogueo.HasValue ? l.FechaDeslogueo.Value.ToString("yyyy-MM-dd HH:mm:ss") : null
        //            })
        //            .ToListAsync();

        //        if (!logueos.Any())
        //        {
        //            return new NotFoundObjectResult("No hay registros de logueo de empleados.");
        //        }

        //        return new OkObjectResult(logueos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
        //    }
        //}
    }
}
