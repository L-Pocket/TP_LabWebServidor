using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "RequireMozoRole")]
        [HttpGet("listadoDeMesas")]
        public async Task<ActionResult<List<Mesa>>> GetMesas()
        {
            var mesas = await _mesaServicio.GetAll();
            return Ok(mesas);
        }


        
    }

}
