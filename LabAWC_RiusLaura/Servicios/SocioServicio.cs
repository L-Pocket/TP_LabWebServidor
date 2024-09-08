using LabAWS_RiusLaura.DTO;
using LabAWC_RiusLaura.DAL.Data;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LabAWS_RiusLaura.Servicios
{
    public interface ISocioServicio
    {
        Task<ActionResult<Mesa>> CerrarMesa(int idMesa);
        Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora();
        Task<ActionResult<IEnumerable<MesaEstadoDto>>> ListarMesasConEstados();
        Task<ActionResult<IEnumerable<CantidadEmpleadosPorSectorDto>>> CantidadEmpleadosPorSector();
        Task<ActionResult<EmpleadoDto>> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId);
        Task<ActionResult<EmpleadoDto>> SuspenderEmpleado(int idEmpleado);
        Task<ActionResult> BorrarEmpleado(int idEmpleado);
        Task<ActionResult<IEnumerable<LogueoEmpleadoDto>>> ListarLogueosEmpleados();
    }

    public class SocioServicio : ISocioServicio
    {
        private readonly DataContext _context;

        public SocioServicio(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<Mesa>> CerrarMesa(int idMesa)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(idMesa);
                if (mesa == null)
                    return new NotFoundObjectResult($"No se encontró una mesa con el id {idMesa}.");

                if (mesa.EstadoDeMesaId == 3)
                {
                    mesa.EstadoDeMesaId = 4;
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(mesa);
                }
                else
                {
                    return new BadRequestObjectResult($"La mesa con el id {idMesa} no está en un estado válido para cerrar.");
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<IEnumerable<PedidoDemoradoDto>>> ListarPedidosConDemora()
        {
            try
            {
                var pedidos = await _context.Pedidos
                    .Where(p => p.EstadoDelPedidoId == 1)
                    .Select(p => new PedidoDemoradoDto
                    {
                        IdPedido = p.IdPedido,
                        ComandaDelPedidoId = p.ComandaDelPedidoId,
                        TiempoEstimado = p.TiempoEstimado,
                        TiempoReal = EF.Functions.DateDiffMinute(p.FechaCreacion, DateTime.Now),
                        Estado = p.EstadoDelPedido.DescripcionPedido
                    })
                    .Where(p => p.TiempoEstimado < p.TiempoReal)
                    .ToListAsync();

                return new OkObjectResult(pedidos);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<IEnumerable<MesaEstadoDto>>> ListarMesasConEstados()
        {
            try
            {
                var mesas = await _context.Mesas
                    .Select(m => new MesaEstadoDto
                    {
                        IdMesa = m.IdMesa,
                        CodigoMesa = m.CodigoMesa,
                        Estado = m.EstadoDeMesa.DescripcionMesa
                    })
                    .ToListAsync();

                if (mesas == null || mesas.Count == 0)
                {
                    return new NotFoundObjectResult("No se encontraron mesas registradas.");
                }

                return new OkObjectResult(mesas);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<IEnumerable<CantidadEmpleadosPorSectorDto>>> CantidadEmpleadosPorSector()
        {
            try
            {
                var resultado = await _context.Empleados
                    .GroupBy(e => e.SectorDelEmpleadoId)
                    .Select(g => new CantidadEmpleadosPorSectorDto
                    {
                        Sector = g.FirstOrDefault().SectorDelEmpleado.DescripcionSector,
                        CantidadEmpleados = g.Count()
                    })
                    .ToListAsync();

                return new OkObjectResult(resultado);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<EmpleadoDto>> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) ||
                sectorDelEmpleadoId <= 0 || rolDelEmpleadoId <= 0)
            {
                return new BadRequestObjectResult("Nombre, usuario, contraseña, sectorDelEmpleadoId y rolDelEmpleadoId son obligatorios y deben ser válidos.");
            }

            try
            {
                var nuevoEmpleado = new Empleado
                {
                    Nombre = nombre,
                    Usuario = usuario,
                    Password = password,
                    SectorDelEmpleadoId = sectorDelEmpleadoId,
                    RolDelEmpleadoId = rolDelEmpleadoId,
                    EmpleadoActivo = true
                };

                _context.Empleados.Add(nuevoEmpleado);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new EmpleadoDto
                {
                    IdEmpleado = nuevoEmpleado.IdEmpleado,
                    Nombre = nuevoEmpleado.Nombre,
                    Usuario = nuevoEmpleado.Usuario,
                    EmpleadoActivo = nuevoEmpleado.EmpleadoActivo
                });
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<EmpleadoDto>> SuspenderEmpleado(int idEmpleado)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(idEmpleado);
                if (empleado == null)
                {
                    return new NotFoundObjectResult($"No se encontró un empleado con el id {idEmpleado}.");
                }

                if (empleado.EmpleadoActivo == false)
                {
                    return new BadRequestObjectResult("El empleado ya se encuentra suspendido.");
                }

                empleado.EmpleadoActivo = false;
                await _context.SaveChangesAsync();

                return new OkObjectResult(new EmpleadoDto
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Nombre = empleado.Nombre,
                    Usuario = empleado.Usuario,
                    EmpleadoActivo = empleado.EmpleadoActivo
                });
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> BorrarEmpleado(int idEmpleado)
        {
            try
            {
                var empleado = await _context.Empleados.FindAsync(idEmpleado);
                if (empleado == null)
                {
                    return new NotFoundObjectResult($"No se encontró un empleado con el id {idEmpleado}.");
                }

                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();

                return new OkObjectResult($"Se eliminó al empleado con el id {idEmpleado}.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<IEnumerable<LogueoEmpleadoDto>>> ListarLogueosEmpleados()
        {
            try
            {
                var logueos = await _context.LogueosEmpleados
                    .Select(l => new LogueoEmpleadoDto
                    {
                        EmpleadoLogId = l.EmpleadoLogId,
                        Nombre = l.EmpleadoLog.Nombre,
                        FechaLogueo = l.FechaLogueo.ToString("yyyy-MM-dd HH:mm:ss"),
                        FechaDeslogueo = l.FechaDeslogueo.HasValue ? l.FechaDeslogueo.Value.ToString("yyyy-MM-dd HH:mm:ss") : null
                    })
                    .ToListAsync();

                if (!logueos.Any())
                {
                    return new NotFoundObjectResult("No hay registros de logueo de empleados.");
                }

                return new OkObjectResult(logueos);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error interno del servidor: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
