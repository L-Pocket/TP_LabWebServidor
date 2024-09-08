using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {

        private readonly IMesaServicio _mesaServicio;

        public MesasController(IMesaServicio mesaServicio)
        {
            _mesaServicio = mesaServicio;
        }

        [HttpGet("listado de mesas")]

        public async Task<ActionResult<List<Mesa>>> GetMesas()
        {
            var resultado = await _mesaServicio.GetAll();
            return resultado;
        }


        /*
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

        */
    }

}
