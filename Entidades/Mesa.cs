using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mesa
    {
        [Key, Required]
        public int IdMesa { get; set; }

        [Required, StringLength(5, MinimumLength = 5)]
        public string CodigoMesa { get; set; } // Las mesas tienen un ID alfanumérico de 5 caracteres

        [Required]
        public int EstadoDeMesaId { get; set; } // FK para EstadoMesa
        public virtual EstadoMesa EstadoDeMesa { get; set; } // 

    }
}
