using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;
using Restaurante_API.DTO;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IPedidoService
    {
        Task<PedidoResponseDto> GetPedidoById(int idPedido);
        Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<PedidoResponseDto> CrearPedido(PedidoCreateDto pedidoDto);
        Task<List<ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId);
    }

    public class PedidoServicio : IPedidoService
    {
        private readonly DataContext _context;
        private readonly ILogger<PedidoServicio> logger;
        private readonly IMapper mapper;

        // Constructor
        public PedidoServicio(DataContext context, ILogger<PedidoServicio> logger, IMapper mapper)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context)); //se lanza una excepción, lo que asegura que el servicio no intente operar con una dependencia no válida.
            this.logger = logger;
            this.mapper = mapper;
        }


        // GET de 1 pedido por su ID 
        public async Task<PedidoResponseDto?> GetPedidoById(int id)
        {
            this.logger.LogInformation($"Buscando pedido con ID: {id}");
            // Buscar el pedido en la BBDD por su ID
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                this.logger.LogWarning($"No se encontró el pedido con ID: {id}");
                return null;
            }

            // Mapear Pedido a PedidoResponseDto para devolverlo al controller
            var pedidoResponseDto = this.mapper.Map<PedidoResponseDto>(pedido);
            this.logger.LogInformation($"Pedido encontrado: {pedidoResponseDto.Id}");

            return pedidoResponseDto;
        }

        //INFORMES PEDIDOS A -  producto más vendido

        public async Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            this.logger.LogInformation("Iniciando la búsqueda del producto más vendido.");

            // Agrupamos los pedidos por el ID del producto y calculamos la cantidad total vendida por producto
            var productoMasVendido = await _context.Pedidos
                .Where(p =>
                    (!fechaInicio.HasValue || p.FechaCreacion.Date >= fechaInicio.Value.Date)  // Filtramos por fecha de inicio sin hora
                    && (!fechaFin.HasValue || p.FechaCreacion.Date <= fechaFin.Value.Date) // Filtramos por fecha de fin sin hora
                )
                .GroupBy(p => p.ProductoId)
                .Select(g => new  // Creamos un nuevo objeto con el ID del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderByDescending(g => g.CantidadVendida) // Ordenamos de forma descendente por la cantidad vendida
                .FirstOrDefaultAsync(); // Obtenemos el producto más vendido o null si no hay datos

            // Si no se encuentra ningún producto vendido, devuelve un mensaje de error
            if (productoMasVendido == null)
            {
                this.logger.LogWarning("No se encontró ningún producto vendido.");
                return null; // Si no se encuentra un producto más vendido, retorna null
            }

            // Busca el producto en la BBDD utilizando el ID obtenido
            var producto = await _context.Productos.FindAsync(productoMasVendido.ProductoId);

            if (producto == null)
            {
                this.logger.LogWarning("El producto no existe en la BBDD");
                return null; // Si el producto no existe, retorna null
            }

            this.logger.LogInformation($"Producto más vendido encontrado: ID {productoMasVendido.ProductoId} - Cantidad Vendida {productoMasVendido.CantidadVendida}");

            // Mapear Producto a ProductoVendidoDto para devolverlo al controller
            var productoMasVendidoDto = this.mapper.Map<ProductoVendidoDto>(producto);

            // Añadir manualmente la cantidad vendida ya que no está en la entidad producto
            productoMasVendidoDto.CantidadVendida = productoMasVendido.CantidadVendida;

            return productoMasVendidoDto;
        }


        //INFORME PEDIDOS B - producto menos vendido

        public async Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            this.logger.LogInformation("Iniciando la búsqueda del producto menos vendido.");

            // Filtro por fechas, si se proporcionan
            var pedidosFiltrados = _context.Pedidos.AsQueryable();

            if (fechaInicio.HasValue)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.FechaCreacion.Date >= fechaInicio.Value.Date); // Filtro por fecha de inicio
            }

            if (fechaFin.HasValue)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.FechaCreacion.Date <= fechaFin.Value.Date); // Filtro por fecha de fin
            }

            // Agrupa los pedidos filtrados por el ID del producto y calcula la cantidad total vendida por producto
            var productoMenosVendido = await pedidosFiltrados
                .GroupBy(p => p.ProductoId)
                .Select(g => new  // Creamos un nuevo objeto con el ID del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada pedido
                })
                .OrderBy(g => g.CantidadVendida) // Ordenamos por la cantidad vendida (ascendente) para obtener el menos vendido
                .FirstOrDefaultAsync(); // Obtenemos el primer resultado menos vendido o null si no hay datos

            // Si no se encuentra ningún producto vendido, devuelve null
            if (productoMenosVendido == null)
            {
                this.logger.LogWarning("No se encontró ningún producto vendido.");
                return null;
            }

            // Busca el producto en la base de datos utilizando el ID obtenido
            var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);

            // Si el producto no existe, devuelve null
            if (producto == null)
            {
                this.logger.LogWarning("El producto no existe en la base de datos.");
                return null;
            }

            this.logger.LogInformation($"Producto menos vendido encontrado: ID {productoMenosVendido.ProductoId} - Cantidad Vendida {productoMenosVendido.CantidadVendida}");

            // Mapear Producto a ProductoVendidoDto para devolverlo al controlador
            var productoMenosVendidoDto = this.mapper.Map<ProductoVendidoDto>(producto);

            // Añadir manualmente la cantidad vendida ya que no está en la entidad producto
            productoMenosVendidoDto.CantidadVendida = productoMenosVendido.CantidadVendida;

            return productoMenosVendidoDto;
        }

        // POST de un nuevo pedido
        public async Task<PedidoResponseDto?> CrearPedido(PedidoCreateDto pedidoDto)
        {
            this.logger.LogInformation("Iniciando la creación de un nuevo pedido.");
            //Verificar si la comanda existe
            //var comandaExistente = await _context.Comandas.FindAsync(pedidoDto.ComandaId);
            var comandaExistente = await _context.Comandas
                .Include(c => c.Mesa)  // Incluye la mesa asociada a la comanda
                .FirstOrDefaultAsync(c => c.Id == pedidoDto.ComandaId);

            if (comandaExistente == null)
            {
                this.logger.LogWarning($"Comanda no encontrada con ID: {pedidoDto.ComandaId}");
                return null; // Si la comanda no existe, retorna null
            }

            //Verificar si el producto existe
            var productoExistente = await _context.Productos.FindAsync(pedidoDto.ProductoId);
            if (productoExistente == null)
            {
                this.logger.LogWarning($"Producto no encontrado con ID: {pedidoDto.ProductoId}");
                return null; // Si el producto no existe, retorna null
            }

            

            // Versión automapper.Mapear pedidoDTO a entidad Pedido:
            Pedido pedido = this.mapper.Map<Pedido>(pedidoDto);

            // Relacionar el pedido con la comanda y la mesa si la comanda existe.
            pedido.Comanda = comandaExistente;
            pedido.ComandaId = comandaExistente.Id;

            _context.Pedidos.Add(pedido); // Añadir el nuevo pedido a la base de datos

            // Modifico la cantidad de productos:
            productoExistente.ReducirStock(pedidoDto.Cantidad);
            _context.Productos.Update(productoExistente);

            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            this.logger.LogInformation("Pedido creado exitosamente.");

            // Mapear Pedido a PedidoResponseDto para devolverlo al controller
            var pedidoResponseDto = this.mapper.Map<PedidoResponseDto>(pedido);

            // Asignar el Código de Mesa a la respuesta, si existe la mesa asociada
            if (comandaExistente.Mesa != null)
            {
                pedidoResponseDto.CodigoMesa = comandaExistente.Mesa.Codigo;
            }

            // Retorna
            return pedidoResponseDto;

        }
        public async Task<List<ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId)
        {
            this.logger.LogInformation("Iniciando la búsqueda del productos pendientes por sector.");

            // Agrupa los productos pendientes por su ID y calcula la cantidad pendiente
            var productosPendientes = await _context.Productos
                                  .Join(_context.Pedidos,//join entre tablas
                                        producto => producto.Id,
                                        pedido => pedido.ProductoId,
                                        (producto, pedido) => new { producto, pedido })
                                  .Where(p => p.producto.SectorId == sectorId
                                         && p.pedido.EstadoPedidoId == 1) // 1 = Estado Pendiente
                                  .GroupBy(p => new { p.producto.Id, p.producto.NombreDesc }) // Agrupamos por ProductoId y Nombre
                                  .Select(g => new
                                  {
                                      ProductoId = g.Key.Id,
                                      Nombre = g.Key.NombreDesc,
                                      CantidadPendiente = g.Sum(p => p.pedido.Cantidad) // Sumamos la cantidad pendiente de cada producto
                                  })
                                  .ToListAsync();                                
                                

            // Mapea los productos pendientes a ProductoVendidoDto
            var productosDto = productosPendientes.Select(p => new ProductoPendienteDto
            {                
                NombreDesc = p.Nombre,
                CantidadPendiente = p.CantidadPendiente // Asigna manualmente la cantidad pendiente

            }).ToList();

            return productosDto;


        }

    }
}
