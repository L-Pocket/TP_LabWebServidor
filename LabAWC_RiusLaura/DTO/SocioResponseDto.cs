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

    public class MesaEstadoDto
    {
        public int IdMesa { get; set; }
        public string CodigoMesa { get; set; }
        public string Estado { get; set; }
    }

    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public bool EmpleadoActivo { get; set; }
    }

    public class LogueoEmpleadoDto
    {
        public int EmpleadoLogId { get; set; }
        public string Nombre { get; set; }
        public string FechaLogueo { get; set; }
        public string FechaDeslogueo { get; set; }
    }

    public class CantidadEmpleadosPorSectorDto
    {
        public string Sector { get; set; }
        public int CantidadEmpleados { get; set; }
    }
}
