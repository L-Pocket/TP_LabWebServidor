using LabAWS_RiusLaura.DTO;
using LabAWC_RiusLaura.DAL.Data;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IEmpleadoServicio
    {
        Task<PedidoResponseDto> PonerPedidoEnPreparacion(int idPedido, int tiempoEstimado);
        Task<PedidoResponseDto> PonerPedidoListoParaServir(int idPedido);
        Task<MesaDto> CambiarEstadoMesaClienteComiendo(int idMesa);
        Task<MesaDto> CambiarEstadoMesaClientePagando(int idMesa);
    }

    public class EmpleadoServicio : IEmpleadoServicio
    {
        private readonly DataContext _context;
        private readonly ILogger<ClienteServicio> logger;
        private readonly IMapper _mapper;

        public EmpleadoServicio(DataContext context, ILogger<ClienteServicio> logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
            this._mapper = mapper;
        }

        public async Task<PedidoResponseDto> PonerPedidoEnPreparacion(int idPedido, int tiempoEstimado)
        {
            this.logger.LogInformation("Iniciando poner pedido en Preparación.");
            // Busca el pedido por ID
            var pedido = await _context.Pedidos.FindAsync(idPedido);

            if (pedido == null)
            {
                this.logger.LogWarning($"No se encontró un pedido con el ID: {idPedido}.");
                throw new KeyNotFoundException($"No se encontró un pedido con el ID: {idPedido}.");
            }

            if (pedido.EstadoDelPedidoId == 1) // 1 = "pendiente"
            {
                pedido.EstadoDelPedidoId = 2; // 2 = "en preparación"
                pedido.TiempoEstimado = tiempoEstimado;
                await _context.SaveChangesAsync();
                this.logger.LogInformation("Acción finalizada con exito.");

                // automapper Pedido a PedidoResponseDto 
                var pedidoResponseDto = _mapper.Map<PedidoResponseDto>(pedido);

                // Retorna 
                return pedidoResponseDto;
            }
            else
            {
                this.logger.LogWarning($"El pedido con el ID {idPedido} ya no está en estado pendiente.");
                throw new InvalidOperationException($"El pedido con el ID {idPedido} ya no está en estado pendiente.");
            }
            
        }

        public async Task<PedidoResponseDto> PonerPedidoListoParaServir(int idPedido)
        {
            this.logger.LogInformation("Iniciando poner pedido Listo para Servir.");
            // Busca el pedido por ID
            var pedido = await _context.Pedidos.FindAsync(idPedido);

            if (pedido == null)
            {
                this.logger.LogWarning($"No se encontró un pedido con el ID: {idPedido}.");
                throw new KeyNotFoundException($"No se encontró un pedido con el ID: {idPedido}.");
            }

            if (pedido.EstadoDelPedidoId == 2) // 2 = "en preparación"
            {
                pedido.EstadoDelPedidoId = 3; // 3 = "Listo para servir"
                pedido.FechaFinalizacion = DateTime.Now; // una vez listo, se establece la hora de finalización
                await _context.SaveChangesAsync();
                this.logger.LogInformation("Acción finalizada con exito.");

                // automapper Pedido a PedidoResponseDto 
                var pedidoResponseDto = _mapper.Map<PedidoResponseDto>(pedido);

                return pedidoResponseDto;
            }
            else
            {
                this.logger.LogWarning($"El pedido con el id {idPedido} no está en estado de preparación.");
                throw new InvalidOperationException($"El pedido con el id {idPedido} no está en estado de preparación.");
            }            
            
        }


        public async Task<MesaDto> CambiarEstadoMesaClienteComiendo(int idMesa)
        {
            
            this.logger.LogInformation($"Iniciando proceso para cambiar estado de la mesa {idMesa} a Cliente Comiendo");
            // Busca la mesa por ID
            var mesa = await _context.Mesas.FindAsync(idMesa);

            if (mesa == null)
            {
                this.logger.LogWarning($"No se encontró una mesa con el id {idMesa}.");
                throw new KeyNotFoundException($"No se encontró una mesa con el id {idMesa}.");                    
            }

            // Buscar todos los pedidos asociados a la mesa y verificar si alguno está "listo para servir"
            var pedidosListosParaServir = await _context.Pedidos
                .Where(p => p.ComandaDelPedido.MesaDeComandaId == idMesa && p.EstadoDelPedidoId == 3) // 3 = "listo para servir"
                .ToListAsync();

            if (pedidosListosParaServir.Any()) // Si encuentra algún pedido listo para servir ya puede cambiar el estado de la mesa
            {
                mesa.EstadoDeMesaId = 2; // 2 = "cliente comiendo"
                    
                await _context.SaveChangesAsync(); // Guardar los cambios
                this.logger.LogInformation($"La mesa {idMesa} ahora está en estado Cliente Comiendo");

                // Cambiar el estado de los pedidos encontrados a "Servido"
                foreach (var pedido in pedidosListosParaServir)
                {
                    pedido.EstadoDelPedidoId = 4; // 4 = "Servido"
                    this.logger.LogInformation($"Pedido {pedido.IdPedido} cambiado a 'Servido'.");
                }
                
                await _context.SaveChangesAsync();// Guardar los cambios

                // automapper Pedido a PedidoResponseDto 
                var mesaResponseDto = _mapper.Map<MesaDto>(mesa);

                return mesaResponseDto;
            }
            else
            {
                this.logger.LogWarning($"No hay pedidos listos para servir en la mesa con el id {idMesa}.");
                throw new InvalidOperationException($"No hay pedidos listos para servir en la mesa con el id {idMesa}.");
            }
            
        }

        public async Task<MesaDto> CambiarEstadoMesaClientePagando(int idMesa)
        {
            this.logger.LogInformation($"Iniciando proceso para cambiar estado de la mesa {idMesa} a Cliente Pagando");
            // Busca la mesa por ID
            var mesa = await _context.Mesas.FindAsync(idMesa);
            if (mesa == null)
            {
                this.logger.LogWarning($"No se encontró una mesa con el id {idMesa}.");
                throw new KeyNotFoundException($"No se encontró una mesa con el id {idMesa}.");
            }

            if (mesa.EstadoDeMesaId == 2) // 2 = "cliente comiendo"
            {
                mesa.EstadoDeMesaId = 3; // 3 = "cliente pagando"
                this.logger.LogInformation($"Pedido {mesa.EstadoDeMesaId} cambiado a Cliente Pagando.");
                await _context.SaveChangesAsync();// Guardar los cambios

                // automapper Pedido a PedidoResponseDto 
                var mesaResponseDto = _mapper.Map<MesaDto>(mesa);

                return mesaResponseDto;
                
            }
            else
            {
                this.logger.LogWarning($"La mesa con el id {idMesa} no se encuentra en el estado cliente Comiendo");
                throw new InvalidOperationException($"La mesa con el id {idMesa} no se encuentra en el estado cliente Comiendo");                
            }
            
            
        }
    }
}
