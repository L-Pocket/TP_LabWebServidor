namespace Restaurante_API.DTO
{
    public class OperacionesEmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string DescripcionSector { get; set; } 
        public int CantidadOperaciones { get; set; }
    }
}
