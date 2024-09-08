﻿using Entidades;
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

    public class PedidoService : IPedidoService
    {
        private readonly DataContext _context;

        // Constructor que recibe el contexto de la BBDD
        public PedidoService(DataContext context)
        {
            _context = context;
        }


        // GET de 1 pedido por su ID 
        public async Task<PedidoResponseDto?> GetPedidoById(int id)
        {
            // Verificar si el ID proporcionado es mayor que 0
            if (id <= 0)
            {
                return null;
            }
            // Buscar el pedido en la BBDD por su ID
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return null;

            // Retorna un nuevo objeto, mapeando la entidad Pedido a PedidoResponseDto
            return new PedidoResponseDto
            {
                IdPedido = pedido.IdPedido,
                ComandaDelPedidoId = pedido.ComandaDelPedidoId,
                ProductoDelPedidoId = pedido.ProductoDelPedidoId,
                Cantidad = pedido.Cantidad,
                EstadoDelPedidoId = pedido.EstadoDelPedidoId,
                FechaCreacion = pedido.FechaCreacion,
                FechaFinalizacion = pedido.FechaFinalizacion,
                TiempoEstimado = pedido.TiempoEstimado,
                ObservacionesDelPedido = pedido.ObservacionesDelPedido
            };
        }

        // GET del producto más vendido
        public async Task<ProductoVendidoDto> GetProductoMasVendido()
        {
            // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
            var productoMasVendido = await _context.Pedidos
                .GroupBy(p => p.ProductoDelPedidoId)
                .Select(g => new  // Seleccionamos el Id del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderByDescending(g => g.CantidadVendida) // aca nos estaria ordenando de forma desc por la cantidad vendida para saber cual es el mas vendido
                .FirstOrDefaultAsync(); // obtenemos el primero resultado mas vendido o null si es que no hay datos 

            // Si no se encuentra ningún producto vendido, devuelve un mensaje de error
            if (productoMasVendido == null)
            {
                return null; // Si no se encuentra un producto más vendido, retorna null
            }

            // Busca el producto en la BBDD utilizando el ID obtenido
            var producto = await _context.Productos.FindAsync(productoMasVendido.ProductoId);

            if (producto == null)
            {
                return null; // Si el producto no existe, retorna null
            }

            // Retorna un nuevo objeto, mapeando la entidad Pedido a PedidoResponseDto
            return new ProductoVendidoDto
            {
                IdProducto = producto.IdProducto,
                NombreDescProducto = producto.NombreDescProducto,
                CantidadVendida = productoMasVendido.CantidadVendida
            };


        }

        // GET del producto menos vendido
        public async Task<ProductoVendidoDto> GetProductoMenosVendido()
        {
            // Agrupa los pedidos por el ID del producto y calcula la cantidad total vendida por producto
            var productoMenosVendido = await _context.Pedidos
                .GroupBy(p => p.ProductoDelPedidoId)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad)
                })
                .OrderBy(g => g.CantidadVendida) // Ordena de forma ascendente para obtener el menos vendido
                .FirstOrDefaultAsync();

            // Si no se encuentra ningún producto vendido, devuelve null
            if (productoMenosVendido == null)
            {
                return null;
            }

            // Busca el producto en la BBDD utilizando el ID obtenido
            var producto = await _context.Productos.FindAsync(productoMenosVendido.ProductoId);

            // Si el producto no existe, devuelve null
            if (producto == null)
            {
                return null;
            }

            // Retorna un nuevo objeto, mapeando la entidad Producto a ProductoVendidoDto
            return new ProductoVendidoDto
            {
                IdProducto = producto.IdProducto,
                NombreDescProducto = producto.NombreDescProducto,
                CantidadVendida = productoMenosVendido.CantidadVendida
            };
        }

        // POST de un nuevo pedido
        public async Task<PedidoResponseDto?> CrearPedido(PedidoCreateDto pedidoDto)
        {
            //Verificar si la comanda existe
            var comandaExistente = await _context.Comandas.FindAsync(pedidoDto.ComandaDelPedidoId);
            if (comandaExistente == null)
            {
                return null; // Si la comanda no existe, retorna null
            }


            //Verificar si el producto existe
            var productoExistente = await _context.Productos.FindAsync(pedidoDto.ProductoDelPedidoId);
            if (productoExistente == null)
            {
                return null; // Si el producto no existe, retorna null
            }

            //Crear una nueva instancia de Pedido con los parámetros proporcionados
            var nuevoPedido = new Pedido
            {
                ComandaDelPedidoId = pedidoDto.ComandaDelPedidoId,
                ProductoDelPedidoId = pedidoDto.ProductoDelPedidoId,
                Cantidad = pedidoDto.Cantidad,
                EstadoDelPedidoId = 1, // Se inicializa en 1 "Pendiente"
                FechaCreacion = pedidoDto.FechaCreacion,
                TiempoEstimado = pedidoDto.TiempoEstimado,
                CodigoCliente = pedidoDto.CodigoCliente,
                ObservacionesDelPedido = pedidoDto.ObservacionesDelPedido
            };

            _context.Pedidos.Add(nuevoPedido); // Añadir el nuevo pedido a la base de datos
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

            return new PedidoResponseDto
            {
                IdPedido = nuevoPedido.IdPedido,
                ComandaDelPedidoId = nuevoPedido.ComandaDelPedidoId,
                ProductoDelPedidoId = nuevoPedido.ProductoDelPedidoId,
                Cantidad = nuevoPedido.Cantidad,
                EstadoDelPedidoId = nuevoPedido.EstadoDelPedidoId,
                FechaCreacion = nuevoPedido.FechaCreacion,
                FechaFinalizacion = nuevoPedido.FechaFinalizacion,
                TiempoEstimado = nuevoPedido.TiempoEstimado,
                ObservacionesDelPedido = nuevoPedido.ObservacionesDelPedido
            };
        }

    }
}
