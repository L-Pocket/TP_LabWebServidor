using Newtonsoft.Json;
using System.ComponentModel;

namespace LabAWS_RiusLaura.DTO
{
    public class PedidoCreateDto
    {
        public int ComandaDelPedidoId { get; set; }
        public int ProductoDelPedidoId { get; set; }
        public int Cantidad { get; set; }
        public string CodigoCliente { get; set; }
        //public string? ObservacionesDelPedido { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("Sin observaciones")]
        public string ObservacionesDelPedido { get; set; } = "Sin observaciones"; // Valor por defecto
    }
}
