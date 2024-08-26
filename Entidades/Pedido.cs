using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }

        [Required]
        public int ComandaDelPedidoId { get; set; } // FK para Comanda
        public virtual Comanda ComandaDelPedido { get; set; } // 

        [Required]
        public int ProductoDelPedidoId { get; set; } // Clave foránea 
        public Producto ProductoDelPedido { get; set; } // 

        [Required]
        public int Cantidad { get; set; } // Cantidad de productos

        [Required]
        public int EstadoDelPedidoId { get; set; } // FK para EstadoPedido
        public virtual EstadoPedido EstadoDelPedido { get; set; } //

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaFinalizacion { get; set; } // puede ser null


        public int TiempoEstimado { get; set; } // Tiempo de preparación estimada que se constrasta con el tiempo real.

        [Required, StringLength(5, MinimumLength = 5)]
        public string CodigoCliente { get; set; } // ID alfanumérico de 5 caracteres 

        public string? ObservacionesDelPedido { get; set; } // puede ser null


    }
}
