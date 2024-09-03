using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    
    public class EstadoPedido
    {
        private int idEstadoPedido;
        private string descripcionPedido;
        private static readonly string[] valoresPermitidos = { "Pendiente", "En Preparacion", "Listo Para Servir", "Servido", "Cancelado" };

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstadoPedido { get => idEstadoPedido; set => idEstadoPedido = value; }

        [Required]
        public string DescripcionPedido 
        { 
            get => descripcionPedido;
            set 
            {
                //comprueba si el valor es igual a value ignorando las diferencias entre mayúsculas y minúsculas
                //gracias StringComparison.OrdinalIgnoreCase.
                if (Array.Exists(valoresPermitidos, rol => rol.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    descripcionPedido = value; // Si la condición es verdadera se asigna el valor proporcionado.
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es válido para DescripcionPedido");
                }
            }               
                
        }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdEstadoPedido { get; set; }
        //[Required]
        //public string DescripcionPedido { get; set; }
    }
}
