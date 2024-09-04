using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly DataContext _context;

        // Constructor
        public MesasController(DataContext context)
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
