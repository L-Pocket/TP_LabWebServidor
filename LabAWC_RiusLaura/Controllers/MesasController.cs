using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using LabAWS_RiusLaura.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante_API.Controllers.Responses;

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

        //[Authorize(Policy = "RequireMozoRole")]
        [HttpGet("listadoDeMesas")]
        public async Task<ActionResult<List<MesaDto>>> GetMesas()
        {
            try
            {
                // Llama al servicio para obtener todas las mesas
                var mesas = await _mesaServicio.GetAll();

                // Verifica si no se encontraron mesas
                if (mesas == null || mesas.Count == 0)
                {
                    return NotFound(new ApiResponse<List<MesaDto>>
                    {
                        Success = false,
                        Message = "No se encontraron mesas en el sistema.",
                        Data = null
                    });
                }

                // Devuelve la lista de mesas 
                return Ok(new ApiResponse<List<MesaDto>>
                {
                    Success = true,
                    Message = "Listado de mesas obtenido exitosamente.",
                    Data = mesas
                });
            }
            catch (Exception ex)
            {                
                // Manejo de errores generales
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener las mesas.",
                    Data = ex.Message
                });
            }
        }


        
    }

}
