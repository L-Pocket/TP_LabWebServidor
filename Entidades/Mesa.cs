using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Mesa
    {

        private int idMesa;
        private string codigoMesa;
        private int estadoDeMesaId;
        private EstadoMesa estadoDeMesa;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMesa { get => idMesa; set => idMesa = value; }

        [Required, StringLength(5, MinimumLength = 5)]
        public string CodigoMesa 
        { 
            get => codigoMesa; 
            set
            {
                if (value.Length != 5)
                {
                    throw new ArgumentException("El código de la mesa debe tener exactamente 5 caracteres.");
                }
                codigoMesa = value;
            }
        }
        [Required]
        public int EstadoDeMesaId { get => estadoDeMesaId; set => estadoDeMesaId = value; } // FK para EstadoMesa
        public EstadoMesa EstadoDeMesa { get => estadoDeMesa; set => estadoDeMesa = value; }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //public int IdMesa { get; set; }
        //[Required, StringLength(5, MinimumLength = 5)]
        //public string CodigoMesa { get; set; } // Las mesas tienen un ID alfanumérico de 5 caracteres
        //[Required]
        //public int EstadoDeMesaId { get; set; } // FK para EstadoMesa
        //public virtual EstadoMesa EstadoDeMesa { get; set; } // 

    }
}
