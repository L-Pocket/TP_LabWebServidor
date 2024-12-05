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
        private int id;
        private int mesaId;
        private Mesa mesa;
        private string nombreCliente;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        public int MesaId { get => mesaId; set => mesaId = value; } // FK para Mesa
        public Mesa Mesa { get => mesa; set => mesa = value; }

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

    }
}
