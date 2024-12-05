using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{

    public class Sector
    {
        private int id;
        private string descripcion;
        private static readonly string[] valoresPermitidos = { "Barra Tragos Y Vino", "Cerveza Artesanal", "Cocina", "Candybar", "General" };

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        public string Descripcion
        {
            get => descripcion;
            set
            {
                //comprueba si el valor es igual a value ignorando las diferencias entre mayúsculas y minúsculas
                //gracias StringComparison.OrdinalIgnoreCase.
                if (Array.Exists(valoresPermitidos, sector => sector.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    descripcion = value; // Si la condición es verdadera se asigna el valor proporcionado.
                }
                else
                {
                    throw new ArgumentException($"El valor '{value}' no es válido para DescripcionSector");
                }
            }

        }

    }
}
