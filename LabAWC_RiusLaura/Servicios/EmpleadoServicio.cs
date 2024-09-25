using LabAWS_RiusLaura.DTO;
using LabAWC_RiusLaura.DAL.Data;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IEmpleadoServicio
    {
        Task<ActionResult> PonerPedidoEnPreparacion(int idPedido, int tiempoEstimado);
        Task<ActionResult> PonerPedidoListoParaServir(int idPedido);
        Task<ActionResult> CambiarEstadoMesaClienteComiendo(int idMesa);
        Task<ActionResult> CambiarEstadoMesaClientePagando(int idMesa);
    }

    public class EmpleadoServicio : IEmpleadoServicio
    {
        private readonly DataContext _context;
        private readonly ILogger<ClienteServicio> logger;

        public EmpleadoServicio(DataContext context, ILogger<ClienteServicio> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;   
        }

        public async Task<ActionResult> PonerPedidoEnPreparacion(int idPedido, int tiempoEstimado)
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

                return new OkObjectResult(new EmpleadoResponseDto
                {
                    Success = true,
                    Message = $"El pedido con el id {idPedido} ha sido puesto en preparación."
                });
            }
            else
            {
                this.logger.LogWarning($"El pedido con el ID {idPedido} ya no está en estado pendiente.");
                return new BadRequestObjectResult(new EmpleadoResponseDto
                {
                    Success = false,
                    Message = $"El pedido con el id {idPedido} ya no está en estado pendiente."
                });
            }
            
        }

        public async Task<ActionResult> PonerPedidoListoParaServir(int idPedido)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(idPedido);
                if (pedido == null)
                {
                    return new NotFoundObjectResult($"No se encontró un pedido con el id {idPedido}.");
                }

                if (pedido.EstadoDelPedidoId == 2) // 2 = "en preparación"
                {
                    pedido.EstadoDelPedidoId = 3; // 3 = "Listo para servir"
                    pedido.FechaFinalizacion = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return new OkObjectResult(new EmpleadoResponseDto
                    {
                        Success = true,
                        Message = $"El pedido con el id {idPedido} ha sido marcado como listo para servir."
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new EmpleadoResponseDto
                    {
                        Success = false,
                        Message = $"El pedido con el id {idPedido} no está en estado de preparación."
                    });
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new EmpleadoResponseDto
                {
                    Success = false,
                    Message = $"Error interno del servidor: {ex.Message}"
                })
                { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> CambiarEstadoMesaClienteComiendo(int idMesa)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(idMesa);
                if (mesa == null)
                {
                    return new NotFoundObjectResult($"No se encontró una mesa con el id {idMesa}.");
                }

                var pedidosListosParaServir = await _context.Pedidos
                    .Where(p => p.ComandaDelPedido.MesaDeComandaId == idMesa && p.EstadoDelPedidoId == 3) // 3 = "listo para servir"
                    .ToListAsync();

                if (pedidosListosParaServir.Any())
                {
                    mesa.EstadoDeMesaId = 2; // 2 = "cliente comiendo"
                    
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(new EmpleadoResponseDto
                    {
                        Success = true,
                        Message = $"La mesa con el id {idMesa} ha sido cambiada a 'Cliente comiendo'."
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new EmpleadoResponseDto
                    {
                        Success = false,
                        Message = $"No hay pedidos listos para servir en la mesa con el id {idMesa}."
                    });
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new EmpleadoResponseDto
                {
                    Success = false,
                    Message = $"Error interno del servidor: {ex.Message}"
                })
                { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> CambiarEstadoMesaClientePagando(int idMesa)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(idMesa);
                if (mesa == null)
                {
                    return new NotFoundObjectResult($"No se encontró una mesa con el id {idMesa}.");
                }

                if (mesa.EstadoDeMesaId == 2) // 2 = "cliente comiendo"
                {
                    mesa.EstadoDeMesaId = 3; // 3 = "cliente pagando"
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(new EmpleadoResponseDto
                    {
                        Success = true,
                        Message = $"La mesa con el id {idMesa} ha sido cambiada a 'Cliente pagando'."
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new EmpleadoResponseDto
                    {
                        Success = false,
                        Message = $"La mesa con el id {idMesa} ya está en el estado de 'Cliente pagando'."
                    });
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new EmpleadoResponseDto
                {
                    Success = false,
                    Message = $"Error interno del servidor: {ex.Message}"
                })
                { StatusCode = 500 };
            }
        }
    }
}
