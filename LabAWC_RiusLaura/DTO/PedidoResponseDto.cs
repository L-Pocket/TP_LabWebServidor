namespace LabAWS_RiusLaura.DTO
{
    public class PedidoResponseDto
    {
        public int IdPedido { get; set; }
        public int ComandaDelPedidoId { get; set; }
        public int ProductoDelPedidoId { get; set; }
        public int Cantidad { get; set; }
        public int EstadoDelPedidoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int TiempoEstimado { get; set; }
        public string? ObservacionesDelPedido { get; set; }
    }
}
