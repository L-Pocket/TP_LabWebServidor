namespace LabAWS_RiusLaura.DTO
{
    public class PedidoDemoradoDto
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int TiempoEstimado { get; set; }
        public double TiempoReal { get; set; }
        public string Estado { get; set; }
    }

    
}
