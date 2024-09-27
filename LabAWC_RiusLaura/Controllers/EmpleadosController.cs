using Entidades;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public EmpleadosController(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        [HttpPut("PonerPedidoEnPreparacion/{idPedido}")]
        public async Task<ActionResult> PonerPedidoEnPreparacion(int idPedido, [FromQuery] int tiempoEstimado)
        {
            // Verifica que todos los parámetros sean válidos
            if (idPedido <= 0 || tiempoEstimado <= 0)
            {
                return BadRequest("Los parámetros 'idPedido' y 'tiempoEstimado' son obligatorios y deben ser mayores que cero.");
            }

            try
            {
                // Llama al servicio para poner el pedido en preparación
                var resultado = await _empleadoServicio.PonerPedidoEnPreparacion(idPedido, tiempoEstimado);
                
                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en pendiente 
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("PonerPedidoListoParaServir/{idPedido}")]
        public async Task<ActionResult> PonerPedidoListoParaServir(int idPedido)
        {           
            
            // Verifica que todos los parámetros sean válidos
            if (idPedido <= 0)
            {
                return BadRequest("idPedidoes obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para poner el pedido listo para servir
                var resultado = await _empleadoServicio.PonerPedidoListoParaServir(idPedido);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no se encuentra el pedido
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si el pedido no está en preparación
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("CambiarEstadoMesaClienteComiendo/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClienteComiendo(int idMesa)
        {
            
            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest("idMesa es obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Comiendo
                var resultado = await _empleadoServicio.CambiarEstadoMesaClienteComiendo(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
                //return Ok($"OK! La mesa con el ID {idMesa} tiene a sus clientes comiendo");
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Esperando Pedido
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("CambiarEstadoMesaClientePagando/{idMesa}")]
        public async Task<ActionResult> CambiarEstadoMesaClientePagando(int idMesa)
        {
            // Verifica que todos los parámetros sean válidos
            if (idMesa <= 0)
            {
                return BadRequest("idMesa es obligatorios y debe ser mayor que cero.");
            }

            try
            {
                // Llama al servicio para cambiar el estado a Cliente Pagando
                var resultado = await _empleadoServicio.CambiarEstadoMesaClientePagando(idMesa);

                // Devuelve un código de estado 200 OK 
                return Ok(resultado);
                //return Ok($"OK! La mesa con el ID {idMesa} está pagando");
            }
            catch (KeyNotFoundException ex)
            {
                // Manejo de errores si no encuentra la mesa
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de errores si la mesa no está en Cliente Comiendo
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = ex.Message });
            }
            
            
        }
    }
}



/*
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Controllers
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

        // GET // Listar todos los productos pendientes de este tipo de empleado
        // Sería un GET ALL productos pendientes de cada Rol 1, 2 y 3 Bartender, cervecero y cocinero.


    }
}
*/