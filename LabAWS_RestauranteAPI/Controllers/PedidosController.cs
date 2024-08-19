using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabAWS_RestauranteAPI.Models;


namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly RestauranteContext _context;
        public PedidosController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            return Ok(await _context.Pedidos.ToListAsync());
        }

    }
}

