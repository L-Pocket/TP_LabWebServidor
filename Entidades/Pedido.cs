using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Pedido    
    {

        private int idPedido;
        private int comandaDelPedidoId;
        private Comanda comandaDelPedido;
        private int productoDelPedidoId;
        private Producto productoDelPedido;
        private int cantidad;
        private int estadoDelPedidoId;
        private EstadoPedido estadoDelPedido;
        private DateTime fechaCreacion;
        private DateTime? fechaFinalizacion;
        private int tiempoEstimado;
        private string codigoCliente;
        private string? observacionesDelPedido;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get => idPedido; set => idPedido = value; }

        [Required]
        public int ComandaDelPedidoId { get => comandaDelPedidoId; set => comandaDelPedidoId = value; } // FK para Comanda
        public Comanda ComandaDelPedido { get => comandaDelPedido; set => comandaDelPedido = value; }

        [Required]
        public int ProductoDelPedidoId { get => productoDelPedidoId; set => productoDelPedidoId = value; } // FK para Producto
        public Producto ProductoDelPedido { get => productoDelPedido; set => productoDelPedido = value; }

        [Required]
        public int Cantidad
        {
            get => cantidad;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("La cantidad debe ser un número positivo.");
                }
                cantidad = value;
            }
        }

        [Required]
        public int EstadoDelPedidoId { get => estadoDelPedidoId; set => estadoDelPedidoId = value; } // FK para EstadoPedido
        public EstadoPedido EstadoDelPedido { get => estadoDelPedido; set => estadoDelPedido = value; }

        [Required]
        public DateTime FechaCreacion
        {
            get => fechaCreacion;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("La fecha de creación no puede ser la fecha predeterminada.");
                }
                fechaCreacion = value;
            }
        }
        public DateTime? FechaFinalizacion
        {
            get => fechaFinalizacion;
            set
            {
                if (value.HasValue && value.Value < fechaCreacion)
                {
                    throw new ArgumentException("La fecha de finalización no puede ser anterior a la fecha de creación.");
                }
                fechaFinalizacion = value;
            }
        }

        [Required]
        public int TiempoEstimado
        {
            get => tiempoEstimado;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El tiempo estimado debe ser un número positivo.");
                }
                tiempoEstimado = value;
            }
        }

        [Required, StringLength(5, MinimumLength = 5)]
        public string CodigoCliente
        {
            get => codigoCliente;
            set
            {
                if (value.Length != 5)
                {
                    throw new ArgumentException("El código del cliente debe tener exactamente 5 caracteres.");
                }
                codigoCliente = value;
            }
        }
        public string? ObservacionesDelPedido { get => observacionesDelPedido; set => observacionesDelPedido = value; }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required] 
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdPedido { get; set; }

        //[Required]
        //public int ComandaDelPedidoId { get; set; } // FK para Comanda
        //public virtual Comanda ComandaDelPedido { get; set; } // 

        //[Required]
        //public int ProductoDelPedidoId { get; set; } // FK para Producto
        //public Producto ProductoDelPedido { get; set; } // 

        //[Required]
        //public int Cantidad { get; set; } // Cantidad de productos

        //[Required]
        //public int EstadoDelPedidoId { get; set; } // FK para EstadoPedido
        //public virtual EstadoPedido EstadoDelPedido {  get; set; } //

        //[Required]
        //public DateTime FechaCreacion { get; set; }

        //public DateTime? FechaFinalizacion { get; set; } // puede ser null

        //[Required]
        //public int TiempoEstimado { get; set; } // Tiempo de preparación estimada que se constrasta con el tiempo real.

        //[Required, StringLength(5,MinimumLength =5)] 
        //public string CodigoCliente { get; set; } // ID alfanumérico de 5 caracteres 

        //public string? ObservacionesDelPedido { get; set; } // puede ser null

    }
}
