using System.ComponentModel.DataAnnotations;

namespace Restaurante_API.DTO
{
    public class EmpleadoCreateDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int SectorDelEmpleadoId { get; set; }  
        public int RolDelEmpleadoId { get; set; }    
        public bool EmpleadoActivo { get; set; }
    }
}
