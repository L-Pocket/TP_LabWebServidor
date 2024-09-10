using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IClienteServicio {
        public Task<(bool Success, string ErrorMessage, Mesa Mesa, Pedido Pedido, ClienteResponseDto Resultado)> GetDemora(string codigoMesa, string idPedido);
    }
    public class ClienteServicio : IClienteServicio
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ClienteServicio(DataContext context, IMapper mapper) //recibe una instancia de DataContext,
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));//se lanza una excepción, lo que asegura que el servicio no intente operar con una dependencia no válida.
            _mapper = mapper;
    }
        public async Task<(bool Success, string ErrorMessage, Mesa Mesa, Pedido Pedido, ClienteResponseDto Resultado)> GetDemora(string codigoMesa, string idPedido)
        {
            //throw new NotImplementedException();

            try
            {
                // Buscar la mesa por código en la BBDD
                var mesa = await _context.Mesas
                    .Where(m => m.CodigoMesa == codigoMesa)
                    .FirstOrDefaultAsync();

                if (mesa == null)
                {
                    return (false, $"No se encontró una mesa con el código {codigoMesa}.", null, null, null);
                }

                // Buscar el pedido por código en la BBDD
                var pedido = await _context.Pedidos
                    .Where(p => p.CodigoCliente == idPedido)
                    .FirstOrDefaultAsync();

                if (pedido == null)
                {
                    return (false,$"No se encontró un pedido con el ID {idPedido}.", null, null, null);
                }

                // Verificar que el pedido esté asociado a la mesa correcta                
                var comanda = await _context.Comandas
                    .Where(c => c.IdComanda == pedido.ComandaDelPedidoId && c.MesaDeComandaId == mesa.IdMesa)
                    .FirstOrDefaultAsync();

                if (comanda == null)
                {
                    return (false,"El pedido no está asociado con la mesa proporcionada.", null, null, null);
                }

                
                // Mapear usando AutoMapper
                var resultadoDto = _mapper.Map<ClienteResponseDto>(pedido);

                return (true, null, mesa, pedido, resultadoDto);

            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}", null, null, null);
            }
        }
    }
}
