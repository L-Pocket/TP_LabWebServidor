namespace Restaurante_API.DTO
{
    public class EmpleadosLogDto
    {
        public int id { get; set; }
        public int empleadoLogId { get; set; }
        public DateTime fechaLogueo { get; set; }
        public DateTime fechaDeslogueo { get; set; }
    }
}
