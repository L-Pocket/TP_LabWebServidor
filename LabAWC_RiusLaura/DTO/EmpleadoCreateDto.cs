namespace Restaurante_API.DTO
{
    public class EmpleadoCreateDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public bool EmpleadoActivo { get; set; }
    }
}
