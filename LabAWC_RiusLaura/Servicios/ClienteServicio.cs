using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IClienteServicio {
        //public Task<(bool Success, string ErrorMessage, Mesa Mesa, Pedido Pedido, ClienteResponseDto Resultado)> GetDemora(string codigoMesa, string idPedido);
        Task<ClienteResponseDto> GetDemoraV2(string codigoMesa, string idPedido);
    }
    public class ClienteServicio : IClienteServicio
    {
        private readonly DataContext _context;
        private readonly ILogger<ClienteServicio> logger;
        private readonly IMapper _mapper;
        public ClienteServicio(DataContext context, ILogger<ClienteServicio> logger, IMapper mapper) //recibe una instancia de DataContext,
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));//se lanza una excepción, lo que asegura que el servicio no intente operar con una dependencia no válida.
            this.logger = logger;
            this._mapper = mapper;
    }
        
        //public async Task<(bool Success, string ErrorMessage, Mesa Mesa, Pedido Pedido, ClienteResponseDto Resultado)> GetDemora(string codigoMesa, string idPedido)
        //{
        //    //throw new NotImplementedException();

        //    try
        //    {
        //        // Buscar la mesa por código en la BBDD
        //        var mesa = await _context.Mesas
        //            .Where(m => m.CodigoMesa == codigoMesa)
        //            .FirstOrDefaultAsync();

        //        if (mesa == null)
        //        {
        //            return (false, $"No se encontró una mesa con el código {codigoMesa}.", null, null, null);
        //        }

        //        // Buscar el pedido por código en la BBDD
        //        var pedido = await _context.Pedidos
        //            .Where(p => p.CodigoCliente == idPedido)
        //            .FirstOrDefaultAsync();

        //        if (pedido == null)
        //        {
        //            return (false,$"No se encontró un pedido con el ID {idPedido}.", null, null, null);
        //        }

        //        // Verificar que el pedido esté asociado a la mesa correcta                
        //        var comanda = await _context.Comandas
        //            .Where(c => c.IdComanda == pedido.ComandaDelPedidoId && c.MesaDeComandaId == mesa.IdMesa)
        //            .FirstOrDefaultAsync();

        //        if (comanda == null)
        //        {
        //            return (false,"El pedido no está asociado con la mesa proporcionada.", null, null, null);
        //        }

                
        //        // Mapear usando AutoMapper
        //        var resultadoDto = _mapper.Map<ClienteResponseDto>(pedido);

        //        return (true, null, mesa, pedido, resultadoDto);

        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"Error interno del servidor: {ex.Message}", null, null, null);
        //    }
        //}

        public async Task<ClienteResponseDto> GetDemoraV2(string codigoMesa, string idPedido)
        {
            this.logger.LogInformation("Iniciando la búsqueda del empleados por sector.");
            // Buscar la mesa por código
            var mesa = await _context.Mesas
                .Where(m => m.CodigoMesa == codigoMesa)
                .FirstOrDefaultAsync();

            if (mesa == null)
            {
                this.logger.LogWarning($"No se encontró una mesa con el código: {codigoMesa}.");
                throw new KeyNotFoundException($"No se encontró una mesa con el código: {codigoMesa}.");
            }

            // Buscar el pedido por el ID
            var pedido = await _context.Pedidos
                .Where(p => p.CodigoCliente == idPedido)
                .FirstOrDefaultAsync();

            if (pedido == null)
            {
                this.logger.LogWarning($"No se encontró el código cliente: {idPedido}.");
                throw new KeyNotFoundException($"No se encontró el código cliente: {idPedido}.");
            }

            // Verificar que el pedido esté asociado con la mesa correcta
            var comanda = await _context.Comandas
                .Where(c => c.IdComanda == pedido.ComandaDelPedidoId && c.MesaDeComandaId == mesa.IdMesa)
                .FirstOrDefaultAsync();

            if (comanda == null)
            {
                this.logger.LogWarning("El pedido no está asociado con la mesa proporcionada.");
                throw new InvalidOperationException("El pedido no está asociado con la mesa proporcionada.");
            }

            // Mapear usando AutoMapper al DTO de cliente
            var resultadoDto = _mapper.Map<ClienteResponseDto>(pedido);

            // Devolver el resultado DTO
            this.logger.LogInformation("Busqueda finalizada con exito.");
            return resultadoDto;
            
            
        }
    }
}
