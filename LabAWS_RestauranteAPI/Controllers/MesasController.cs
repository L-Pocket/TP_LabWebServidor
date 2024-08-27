using LabAWS_RestauranteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {

        private readonly RestauranteContext _context;

        // Constructor
        public MesasController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpGet("listado de mesas")]

        public async Task<ActionResult<List<Mesa>>> GetMesas()
        {
            return Ok(await _context.Mesas.ToListAsync());
        }




    }
}
