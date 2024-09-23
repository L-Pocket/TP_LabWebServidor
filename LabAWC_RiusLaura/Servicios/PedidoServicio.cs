using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IPedidoService
    {
        Task<PedidoResponseDto> GetPedidoById(int idPedido);
        Task<ProductoVendidoDto> GetProductoMasVendido();
        Task<ProductoVendidoDto> GetProductoMenosVendido();
        Task<PedidoResponseDto> CrearPedido(PedidoCreateDto pedidoDto);
    }

    public class PedidoServicio : IPedidoService
    {
        private readonly DataContext _context;
        private readonly ILogger<PedidoServicio> logger;
        private readonly IMapper mapper;

        // Constructor
        public PedidoServicio(DataContext context, ILogger<PedidoServicio> logger, IMapper mapper)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
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
            this.logger.LogInformation($"Pedido encontrado: {pedidoResponseDto.IdPedido}");

            return pedidoResponseDto;
        }

        // GET del producto más vendido
        public async Task<ProductoVendidoDto> GetProductoMasVendido()
        {
            this.logger.LogInformation("Iniciando la búsqueda del producto más vendido.");
            // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
            var productoMasVendido = await _context.Pedidos
                .GroupBy(p => p.ProductoDelPedidoId)
                .Select(g => new  // creamos un nuevo objeto anónimo con dos propiedades el Id del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderByDescending(g => g.CantidadVendida) // aca nos estaria ordenando de forma desc por la cantidad vendida para saber cual es el mas vendido
                .FirstOrDefaultAsync(); // obtenemos el primero resultado mas vendido o null si es que no hay datos 

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

            // Codigoo viejo, mapeando en el service
            //// Retorna un nuevo objeto, mapeando la entidad Pedido a PedidoResponseDto
            //return new ProductoVendidoDto
            //{
            //    IdProducto = producto.IdProducto,
            //    NombreDescProducto = producto.NombreDescProducto,
            //    CantidadVendida = productoMasVendido.CantidadVendida
            //};

        }

        // GET del producto menos vendido
        public async Task<ProductoVendidoDto> GetProductoMenosVendido()
        {
            this.logger.LogInformation("Iniciando la búsqueda del producto menos vendido.");
            // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
            var productoMenosVendido = await _context.Pedidos
                .GroupBy(p => p.ProductoDelPedidoId)
                .Select(g => new // creamos un nuevo objeto anónimo con dos propiedades el Id del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad)
                })
                .OrderBy(g => g.CantidadVendida) // Ordena de forma ascendente para obtener el menos vendido
                .FirstOrDefaultAsync(); // obtenemos el primero resultado menos vendido o null si es que no hay datos 

            // Si no se encuentra ningún producto vendido, devuelve null
            if (productoMenosVendido == null)
            {
                this.logger.LogWarning("No se encontró ningún producto vendido.");
                return null;
            }

            logger.LogInformation($"Producto menos vendido encontrado: ID {productoMenosVendido.ProductoId} - Cantidad Vendida {productoMenosVendido.CantidadVendida}");

            // Busca el producto en la BBDD utilizando el ID obtenido
            var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);

            // Si el producto no existe, devuelve null
            if (producto == null)
            {
                this.logger.LogWarning("El producto no existe en la BBDD");
                return null;
            }

            // Mapear Producto a ProductoVendidoDto para devolverlo al controller
            var productoMenosVendidoDto = this.mapper.Map<ProductoVendidoDto>(producto);

            // Añadir manualmente la cantidad vendida ya que no está en la entidad producto
            productoMenosVendidoDto.CantidadVendida = productoMenosVendido.CantidadVendida;

            return productoMenosVendidoDto;

            // Codigoo viejo, mapeando en el service
            //// Retorna un nuevo objeto, mapeando la entidad Producto a ProductoVendidoDto
            //return new ProductoVendidoDto
            //{
            //    IdProducto = producto.IdProducto,
            //    NombreDescProducto = producto.NombreDescProducto,
            //    CantidadVendida = productoMenosVendido.CantidadVendida
            //};
        }

        // POST de un nuevo pedido
        public async Task<PedidoResponseDto?> CrearPedido(PedidoCreateDto pedidoDto)
        {
            this.logger.LogInformation("Iniciando la creación de un nuevo pedido.");
            //Verificar si la comanda existe
            var comandaExistente = await _context.Comandas.FindAsync(pedidoDto.ComandaDelPedidoId);
            if (comandaExistente == null)
            {
                this.logger.LogWarning($"Comanda no encontrada con ID: {pedidoDto.ComandaDelPedidoId}");
                return null; // Si la comanda no existe, retorna null
            }

            //Verificar si el producto existe
            var productoExistente = await _context.Productos.FindAsync(pedidoDto.ProductoDelPedidoId);
            if (productoExistente == null)
            {
                this.logger.LogWarning($"Producto no encontrado con ID: {pedidoDto.ProductoDelPedidoId}");
                return null; // Si el producto no existe, retorna null
            }

            //Versión mapeo manual:
            //Pedido pedido = this.mapper.ConvertirAEntidad(pedidoDto); 

            // Versión automapper.Mapear pedidoDTO a entidad Pedido:
            Pedido pedido = this.mapper.Map<Pedido>(pedidoDto);

            _context.Pedidos.Add(pedido); // Añadir el nuevo pedido a la base de datos

            // Modifico la cantidad de productos:
            productoExistente.ReducirStock(pedidoDto.Cantidad);
            _context.Productos.Update(productoExistente);

            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            this.logger.LogInformation("Pedido creado exitosamente.");

            // Mapear Pedido a PedidoResponseDto para devolverlo al controller
            var pedidoResponseDto = this.mapper.Map<PedidoResponseDto>(pedido);

            // Retorna
            return pedidoResponseDto;

        }

    }
}
