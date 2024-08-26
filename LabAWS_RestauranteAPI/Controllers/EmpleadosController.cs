﻿using LabAWS_RestauranteAPI.DAL;
using LabAWS_RestauranteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpleadosController : ControllerBase
    {
        private readonly DataContext _context;

        // Constructor
        public EmpleadosController(DataContext context)
        {
            _context = context;
        }

        // ----- Listado de funcionalidades mínimas evaluadas:-----

        // GET // Listar todos los productos pendientes de este tipo de empleado
        // Falta resolver agregar FK de id_empleado a el Pedido


        // PUT // Debe cambiar el estado a “en preparación” y agregarle el tiempo de preparación (NUEVO ATRIBUTO DE TiempoEstimado). 
        [HttpPut("PonerPedidoEnPreparacion/{idPedido}")]
        public async Task<ActionResult<Pedido>> PonerPedidoEnPreparacion(int idPedido, int tiempoEstimado)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(idPedido); // Busca el pedido en BBDD
                if (pedido == null)
                    return NotFound($"No se encontró un pedido con el id {idPedido}.");

                if (pedido.EstadoDelPedidoId == 1) // 1 = "pendiente"
                {
                    pedido.EstadoDelPedidoId = 2; // 2 = "en preparación"
                    pedido.TiempoEstimado = tiempoEstimado; // Modifica el tiempo estimado que por default se crea en cero.
                    await _context.SaveChangesAsync(); // Guarda los cambios

                    return Ok(pedido); // si la operacion se realizó con éxito
                }
                else
                {
                    return BadRequest($"El pedido con el id {idPedido} ya no se encuentra pendiente.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        // PUT // Debe cambiar el estado de un pedido “listo para servir”. 
        [HttpPut("PonerPedidoListoParaServir/{idPedido}")]
        public async Task<ActionResult<Pedido>> PonerPedidoListoParaServir(int idPedido)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(idPedido); // Busca el pedido en BBDD
                if (pedido == null)
                    return NotFound($"No se encontró un pedido con el id {idPedido}.");

                if (pedido.EstadoDelPedidoId == 2) // 2 = "en preparación"
                {
                    pedido.EstadoDelPedidoId = 3; // 3 = "Listo para servir"                    
                    await _context.SaveChangesAsync(); // Guarda los cambios

                    return Ok(pedido); // si la operacion se realizó con éxito
                }
                else
                {
                    return BadRequest($"El pedido con el id {idPedido} ya no se encuentra en preparación.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        // PUT // La moza se fija los pedidos que están listos para servir, cambia el estado de la mesa.        
        [HttpPut("CambiarEstadoMesaClienteComiendo/{idMesa}")]
        public async Task<ActionResult<Mesa>> CambiarEstadoMesaClienteComiendo(int idMesa)
        {
            try
            {
                // Busca la mesa en BBDD
                var mesa = await _context.Mesas.FindAsync(idMesa);
                if (mesa == null)
                    return NotFound($"No se encontró una mesa con el id {idMesa}.");

                // Buscar todos los pedidos asociados a la mesa y verificar si alguno está "listo para servir"
                var pedidosListosParaServir = await _context.Pedidos
                    .Where(p => p.ComandaDelPedido.MesaDeComandaId == idMesa && p.EstadoDelPedidoId == 3) // 3 = "listo para servir"
                    .ToListAsync();

                if (pedidosListosParaServir.Any()) // Si encuentra algún pedido listo para servir ya puede cambiar el estado de la mesa
                {
                    // Cambiar el estado de la mesa a "cliente comiendo"
                    mesa.EstadoDeMesaId = 2; // 2 = "cliente comiendo"
                    await _context.SaveChangesAsync(); // Guardar los cambios

                    return Ok(mesa); // Retornar la mesa si la operación fue exitosa
                }
                else
                {
                    return BadRequest($"No hay pedidos listos para servir en la mesa con el id {idMesa}.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        // PUT // La moza cobra la cuenta. // Descripción: cambia a "Cliente pagando".        
        [HttpPut("CambiarEstadoMesaClientePagando/{idMesa}")]
        public async Task<ActionResult<Mesa>> CambiarEstadoMesaClientePagando(int idMesa)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(idMesa); // Busca la mesa en BBDD
                if (mesa == null)
                    return NotFound($"No se encontró una mesa con el id {idMesa}.");

                if (mesa.EstadoDeMesaId == 2) // 2 = "cliente comiendo"
                {
                    mesa.EstadoDeMesaId = 3; // 3 = cliente pagando"                    
                    await _context.SaveChangesAsync(); // Guarda los cambios

                    return Ok(mesa); // si la operacion se realizó con éxito
                }
                else
                {
                    return BadRequest($"La mesa con el id {idMesa} ya no se encuentra comiendo.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




    }
}
