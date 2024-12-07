namespace Restaurante_API.DTO
{
    public class EmpleadosLogDto
    {
       
        public int empleadoLogId { get; set; }
        public DateTime fechaLogueo { get; set; }
        public DateTime? fechaDeslogueo { get; set; }
        public string EmpleadoNombre { get; set; }
    }
}
