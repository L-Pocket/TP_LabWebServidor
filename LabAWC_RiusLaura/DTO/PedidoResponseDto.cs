namespace LabAWS_RiusLaura.DTO
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        //public int ComandaId { get; set; }
        //public int ProductoId { get; set; }
        //public int Cantidad { get; set; }
        public string CodigoCliente { get; set; }
        public string? CodigoMesa { get; set; }

        //public int EstadoPedidoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        //public DateTime? FechaFinalizacion { get; set; }
        //public int TiempoEstimado { get; set; }
        public string? Observaciones { get; set; }
    }
}
