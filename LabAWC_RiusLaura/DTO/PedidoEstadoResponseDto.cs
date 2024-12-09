namespace Restaurante_API.DTO
{
    public class PedidoEstadoResponseDto
    {
        public int Id { get; set; }         
        //public int EstadoPedidoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int TiempoEstimado { get; set; }
        public string? Observaciones { get; set; }
    }
}
