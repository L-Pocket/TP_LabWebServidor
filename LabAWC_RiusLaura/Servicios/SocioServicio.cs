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
        Task<bool> CerrarMesa(int idMesa);        
        Task<EmpleadoCreateDto> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId);
        Task<bool> SuspenderEmpleado(int idEmpleado);
        Task<bool> BorrarEmpleado(int idEmpleado);
        Task<IEnumerable<EmpleadosPorSectorResponseDto>> CantidadEmpleadosPorSector();
        Task<IEnumerable<OperacionesPorSectorDto>> CantidadOperacionesPorSector(int idSector);
        Task<IEnumerable<OperacionesEmpleadoDto>> ObtenerTodasLasOperacionesEmpleados();
        Task<IEnumerable<OperacionesEmpleadoDto>> OperacionesPorEmpleado(int idEmpleado);
        Task<IEnumerable<PedidoDemoradoDto>> ListarPedidosConDemora(); 
        
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

        public async Task<bool> CerrarMesa(int idMesa)
        {
            this.logger.LogInformation("Iniciando cerrar mesa.");
            // Buscar la mesa en la BBDD por su ID
            var mesaEnt = await _context.Mesas.FindAsync(idMesa);
            if (mesaEnt == null)
            {
                this.logger.LogWarning($"No se encontró la mesa con ID: {idMesa}");
                return false;
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
                return true;

            }
            else
            {
                this.logger.LogWarning($"La mesa con el id {idMesa} no está en un estado válido para cerrar.");
                return false;
            }

        }

        public async Task<EmpleadoCreateDto> AgregarEmpleado(string nombre, string usuario, string password, int sectorDelEmpleadoId, int rolDelEmpleadoId)
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
            var nuevoEmpleado = new Empleado
            {
                Nombre = nombre,
                Usuario = usuario,
                EmpleadoActivo = true, // El empleado siempre se crea como activo
                SectorDelEmpleadoId = sectorDelEmpleadoId,
                RolDelEmpleadoId = rolDelEmpleadoId,
                Password = password 
            };            

            // Agregar el nuevo empleado a la BBDD
            this.logger.LogInformation("Iniciando inserción del nuevo Empleado.");
            _context.Empleados.Add(nuevoEmpleado);
            await _context.SaveChangesAsync();
            this.logger.LogInformation($"Empleado creado exitosamente con ID: {nuevoEmpleado.IdEmpleado}");

            // Mapear la entidad Empleado a empleadoDTO para devolverlo al controller
            var empleadoDto = this.mapper.Map<EmpleadoCreateDto>(nuevoEmpleado);

            // Retorna
            return empleadoDto;

        }

        public async Task<bool> SuspenderEmpleado(int idEmpleado)
        {
            this.logger.LogInformation("Iniciando Suspender Empleado.");

            // Buscar al empleado en la BBDD por su ID
            var empleado = await _context.Empleados.FindAsync(idEmpleado);

            if (empleado == null)
            {
                this.logger.LogWarning($"No se encontró el empleado con ID: {idEmpleado}");
                return false;
            }

            if (empleado.EmpleadoActivo == false)
            {
                this.logger.LogWarning($"El empleado con ID: {idEmpleado} ya se encuentra suspendido.");
                return false;
            }

            empleado.EmpleadoActivo = false;
            await _context.SaveChangesAsync();
            this.logger.LogInformation("Empleado suspendido exitosamente.");

            return true;

        }

        public async Task<bool> BorrarEmpleado(int idEmpleado)
        {
            this.logger.LogInformation("Iniciando Eliminar Empleado.");

            // Buscar al empleado en la BBDD por su ID
            var empleado = await _context.Empleados.FindAsync(idEmpleado);

            if (empleado == null)
            {
                this.logger.LogWarning($"No se encontró el empleado con ID: {idEmpleado}");
                return false;
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            this.logger.LogInformation("Empleado eliminado exitosamente.");

            return true;

        }

        public async Task<IEnumerable<EmpleadosPorSectorResponseDto>> CantidadEmpleadosPorSector()
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

        //CANTIDAD DE OPERACIONES POR SECTOR (Cuantos pedidos se hicieron para "x" sector)
        public async Task<IEnumerable<OperacionesPorSectorDto>> CantidadOperacionesPorSector(int idSector)
        {
            this.logger.LogInformation("Iniciando la búsqueda del operaciones por sector.");

            var operaciones = await _context.Pedidos
                .Join(
                    _context.Productos,
                    pedido => pedido.ProductoDelPedidoId,
                    producto => producto.IdProducto,
                    (pedido, producto) => new { pedido, producto })
                .Where(pp => pp.producto.SectorProductoId == idSector)
                .GroupBy(pp => pp.producto.SectorProducto.DescripcionSector)
                .Select(g => new OperacionesPorSectorDto // dto
                {
                    DescripcionSector = g.Key,
                    CantidadOperaciones = g.Count()
                })
                .ToListAsync();

            // Si no se encuentra devuelve un mensaje de error
            if (operaciones == null)
            {
                this.logger.LogWarning("No se encontró ninguna operación.");
                return null;
            }

            this.logger.LogInformation("Busqueda finalizada con exito.");
            return operaciones;

            // Mapeo usando AutoMapper
            //var resultado = this.mapper.Map<IEnumerable<OperacionesPorSectorDto>>(operaciones);
            //return resultado;
        }




        //cantidad de operaciones de todos por sector listada por cada empleado (c)

        public async Task<IEnumerable<OperacionesEmpleadoDto>> ObtenerTodasLasOperacionesEmpleados()
        {
            var resultado = await (from emp in _context.Empleados
                                   join sec in _context.Sectores on emp.SectorDelEmpleadoId equals sec.IdSector
                                   join prod in _context.Productos on sec.IdSector equals prod.SectorProductoId
                                   join ped in _context.Pedidos on prod.IdProducto equals ped.ProductoDelPedidoId
                                   group new { emp, sec } by new
                                   {
                                       emp.IdEmpleado,
                                       emp.Nombre,
                                       sec.DescripcionSector // Incluir la descripción del sector
                                   } into empGroup
                                   select new OperacionesEmpleadoDto
                                   {
                                       IdEmpleado = empGroup.Key.IdEmpleado,
                                       Nombre = empGroup.Key.Nombre,
                                       DescripcionSector = empGroup.Key.DescripcionSector, // Asignar descripción del sector
                                       CantidadOperaciones = empGroup.Count() // Contar el número de pedidos
                                   }).OrderByDescending(e => e.CantidadOperaciones).ToListAsync();

            // Si no se encuentra devuelve un mensaje de error
            if (resultado == null)
            {
                this.logger.LogWarning("No se encontró ninguna operación.");
                return null;
            }

            this.logger.LogInformation("Busqueda finalizada con exito.");
            return resultado;
        }


        // //cantidad de operaciones de cada uno por separado (d)
        public async Task<IEnumerable<OperacionesEmpleadoDto>> OperacionesPorEmpleado(int idEmpleado)
        {
            var resultado = await (from emp in _context.Empleados
                                   join sec in _context.Sectores on emp.SectorDelEmpleadoId equals sec.IdSector
                                   join prod in _context.Productos on sec.IdSector equals prod.SectorProductoId
                                   join ped in _context.Pedidos on prod.IdProducto equals ped.ProductoDelPedidoId
                                   where emp.IdEmpleado == idEmpleado
                                   group new { emp, sec } by new
                                   {
                                       emp.IdEmpleado,
                                       emp.Nombre,
                                       sec.DescripcionSector
                                   } into empGroup
                                   select new OperacionesEmpleadoDto
                                   {
                                       IdEmpleado = empGroup.Key.IdEmpleado,
                                       Nombre = empGroup.Key.Nombre,
                                       DescripcionSector = empGroup.Key.DescripcionSector,
                                       CantidadOperaciones = empGroup.Count() // Cuenta el número de pedidos
                                   }).OrderByDescending(e => e.CantidadOperaciones).ToListAsync();

            // Si no se encuentra devuelve un mensaje de error
            if (resultado == null)
            {
                this.logger.LogWarning("No se encontró ninguna operación.");
                return null;
            }

            this.logger.LogInformation("Busqueda finalizada con exito.");
            return resultado;
        }

        public async Task<IEnumerable<PedidoDemoradoDto>> ListarPedidosConDemora()
        {
            
            var pedidos = await _context.Pedidos // Busca Pedidos en la BBDD
                .Where(p => p.EstadoDelPedidoId == 1) // Filtrar por pedidos pendientes
                .Select(p => new PedidoDemoradoDto //nuevo objeto con los siguientes campos:
                {
                    IdPedido = p.IdPedido,
                    ComandaDelPedidoId = p.ComandaDelPedidoId,
                    TiempoEstimado = p.TiempoEstimado,
                    TiempoReal = EF.Functions.DateDiffMinute(p.FechaCreacion, DateTime.Now), // Calcular la diferencia en minutos
                    Estado = p.EstadoDelPedido.DescripcionPedido
                })
                .Where(p => p.TiempoEstimado < p.TiempoReal) // Filtrar aquellos que están demorados
                .ToListAsync();

            // Si no se encuentra devuelve un mensaje de error
            if (pedidos == null)
            {
                this.logger.LogWarning("No hay pedidos demorados.");
                return null;
            }

            return pedidos;
            
        }

    }
}
