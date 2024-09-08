using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IMesaServicio
    {
        public Task<List<Mesa>> GetAll();
    }
    public class MesaServicio : IMesaServicio
    {


        private readonly DataContext _context;
        public MesaServicio(DataContext context) //recibe una instancia de DataContext,
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));//se lanza una excepción, lo que asegura que el servicio no intente operar con una dependencia no válida.
        }

        public async Task<List<Mesa>> GetAll()
        {
            var resultado = await _context.Mesas.ToListAsync();
            return resultado;
        }
    }
}
