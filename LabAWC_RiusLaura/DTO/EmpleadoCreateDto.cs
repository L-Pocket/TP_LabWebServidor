using System.ComponentModel.DataAnnotations;

namespace Restaurante_API.DTO
{
    public class EmpleadoCreateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int SectorId { get; set; }
        public int RolId { get; set; }
        public bool EmpleadoActivo { get; set; }
    }
}
