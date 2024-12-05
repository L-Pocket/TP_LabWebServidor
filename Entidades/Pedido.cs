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
        public Pedido()
        {
            this.FechaCreacion = DateTime.Now;
            this.estadoPedidoId = 1; // Se inicializa en 1 "Pendiente"
            this.tiempoEstimado = 0; // Se inicializa en cero ya que después e preparación se le asigna un tiempo.
            this.codigoCliente = GenerarCodigo(); // Asignar un valor por defecto al crear la instancia.
            this.Observaciones = "Sin observaciones";
        }

        private int id;
        private int comandaId;
        private Comanda comanda;
        private int productoId;
        private Producto producto;
        private int cantidad;
        private int estadoPedidoId;
        private EstadoPedido estadoPedido;
        private DateTime fechaCreacion;
        private DateTime? fechaFinalizacion;
        private int tiempoEstimado;
        private string codigoCliente;
        private string? observaciones;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        public int ComandaId { get => comandaId; set => comandaId = value; } // FK para Comanda
        public Comanda Comanda { get => comanda; set => comanda = value; }

        [Required]
        public int ProductoId { get => productoId; set => productoId = value; } // FK para Producto
        public Producto Producto { get => producto; set => producto = value; }

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
        public int EstadoPedidoId { get => estadoPedidoId; set => estadoPedidoId = value; } // FK para EstadoPedido
        public EstadoPedido EstadoPedido { get => estadoPedido; set => estadoPedido = value; }

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
                if (value < 0)
                {
                    throw new ArgumentException("El tiempo estimado no puede ser menor a cero.");
                }
                tiempoEstimado = value;
            }
        }

        [Required, StringLength(5, MinimumLength = 5)]
        public string CodigoCliente
        {
            get => codigoCliente;
            private set => codigoCliente = value; // Solo el constructor pueda establecer el valor.
            //get => codigoCliente;
            //set
            //{
            //    if (value.Length != 5)
            //    {
            //        throw new ArgumentException("El código del cliente debe tener exactamente 5 caracteres.");
            //    }
            //    codigoCliente = value;
            //}
        }
        public string? Observaciones { get => observaciones; set => observaciones = value; }

        // Método para generar un código alfanumérico de 5 caracteres.
        private string GenerarCodigo()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(caracteres, 5)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }


    }
}
