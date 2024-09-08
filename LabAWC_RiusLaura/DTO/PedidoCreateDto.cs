namespace LabAWS_RiusLaura.DTO
{
    public class PedidoCreateDto
    {
        public int ComandaDelPedidoId { get; set; }
        public int ProductoDelPedidoId { get; set; }
        public int Cantidad { get; set; }
        public int EstadoDelPedidoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TiempoEstimado { get; set; }
        public string CodigoCliente { get; set; }
        public string? ObservacionesDelPedido { get; set; }
    }
}
