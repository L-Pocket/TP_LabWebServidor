using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Comanda

    {
        private int idComanda;
        private int mesaDeComandaId;
        private Mesa mesaDeComanda;
        private string nombreCliente;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComanda { get => idComanda; set => idComanda = value; }

        [Required]
        public int MesaDeComandaId { get => mesaDeComandaId; set => mesaDeComandaId = value; } // FK para Mesa
        public Mesa MesaDeComanda { get => mesaDeComanda; set => mesaDeComanda = value; }

        [Required, MaxLength(50)]
        public string NombreCliente 
        {
            get => nombreCliente;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre del cliente no puede estar vacío.");
                }
                nombreCliente = value;
            }
        }

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdComanda { get; set; }
        //[Required]
        //public int MesaDeComandaId { get; set; } // FK para Mesa
        //public virtual Mesa MesaDeComanda { get; set; } 
        //[Required, MaxLength(50)]
        //public string NombreCliente { get; set; }

    }
}
