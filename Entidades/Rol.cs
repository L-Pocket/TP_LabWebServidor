using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    
    public class Rol
    {        
        private int idRol;
        private string descripcionRol;
        private static readonly string[] valoresPermitidos = { "Bartender", "Cervecero", "Cocinero", "Mozo", "Socio" };

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get => idRol; set => idRol = value; }

        [Required]
        public string DescripcionRol
        {
            get => descripcionRol;
            set
            {
                //comprueba si el valor de rol es igual a value ignorando las diferencias entre mayúsculas y minúsculas
                //gracias StringComparison.OrdinalIgnoreCase.
                if (Array.Exists(valoresPermitidos, rol => rol.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    descripcionRol = value; // Si la condición es verdadera se asigna el valor proporcionado.
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es válido para DescripcionRol");
                }
            }
        }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required] 
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdRol { get; set; }
        //[Required]
        //public string DescripcionRol { get; set; }
    }
}
