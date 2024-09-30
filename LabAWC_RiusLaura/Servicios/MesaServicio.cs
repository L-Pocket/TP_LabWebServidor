using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IMesaServicio
    {
        public Task<List<MesaDto>> GetAll();
    }
    public class MesaServicio : IMesaServicio
    {


        private readonly DataContext _context;
        private readonly ILogger<MesaServicio> logger;
        private readonly IMapper mapper;
        public MesaServicio(DataContext context, ILogger<MesaServicio> logger, IMapper mapper) //recibe una instancia de DataContext,
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<MesaDto>> GetAll()
        {

            var mesas = await _context.Mesas
               .Include(m => m.EstadoDeMesa) // Incluir la relación con Estados_Mesas
               .ToListAsync();

            var resultado = mapper.Map<List<MesaDto>>(mesas); // Mapear a MesaDto
            return resultado;

            /*

            var mesas = await _context.Mesas.ToListAsync();
            var resultado = mapper.Map<List<MesaDto>>(mesas);
            return resultado;

            */

            /*public async Task<List<Mesa>> GetAll()
            {
                var resultado = await _context.Mesas.ToListAsync();
                return resultado;
            }
            */
        }
    }
}
