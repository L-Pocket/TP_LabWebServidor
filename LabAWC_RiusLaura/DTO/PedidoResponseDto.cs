namespace LabAWS_RiusLaura.DTO
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int MesaId { get; set; }        
        public string CodigoCliente { get; set; }
        public string? CodigoMesa { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        
        public string? Observaciones { get; set; }
    }
}
