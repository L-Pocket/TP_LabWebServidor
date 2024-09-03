using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    
    public class EstadoMesa
    {
        private int idEstadoMesa;
        private string descripcionMesa;
        private static readonly string[] valoresPermitidos = { "Cliente Esperando Pedido", "Cliente Comiendo", "Cliente Pagando", "Cerrada" };

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstadoMesa { get => idEstadoMesa; set => idEstadoMesa = value; }

        [Required]
        public string DescripcionMesa {
            get => descripcionMesa; 
            set 
            {
                //comprueba si el valor es igual a value ignorando las diferencias entre mayúsculas y minúsculas
                //gracias StringComparison.OrdinalIgnoreCase.
                if (Array.Exists(valoresPermitidos, rol => rol.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    descripcionMesa = value; // Si la condición es verdadera se asigna el valor proporcionado.
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es un estado válido para DescripcionMesa");
                }
            }
        }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdEstadoMesa { get; set; }
        //[Required]
        //public string DescripcionMesa { get; set; }
    }
}
