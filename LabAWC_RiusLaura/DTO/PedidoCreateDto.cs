using Newtonsoft.Json;
using System.ComponentModel;

namespace LabAWS_RiusLaura.DTO
{
    public class PedidoCreateDto
    {
        public int ComandaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }        

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue("Sin observaciones")]
        public string Observaciones { get; set; } = "Sin observaciones"; // Valor por defecto
    }
}
