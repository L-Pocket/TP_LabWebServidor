namespace LabAWS_RiusLaura.DTO
{
    public class PedidoDemoradoDto
    {
        public int IdPedido { get; set; }
        public int ComandaDelPedidoId { get; set; }
        public int TiempoEstimado { get; set; }
        public double TiempoReal { get; set; }
        public string Estado { get; set; }
    }

    

    //public class LogueoEmpleadoDto
    //{
    //    public int EmpleadoLogId { get; set; }
    //    public string Nombre { get; set; }
    //    public string FechaLogueo { get; set; }
    //    public string FechaDeslogueo { get; set; }
    //}

    
}
