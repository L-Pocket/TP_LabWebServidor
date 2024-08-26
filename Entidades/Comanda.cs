using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comanda

    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComanda { get; set; }

        [Required]
        public int MesaDeComandaId { get; set; } // FK para Mesa
        public virtual Mesa MesaDeComanda { get; set; } // 

        [Required, MaxLength(50)]
        public string NombreCliente { get; set; }

    }
}
