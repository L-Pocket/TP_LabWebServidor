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
        public Mesa()
        {
            this.codigo = GenerarCodigo(); // Asignar un valor por defecto al crear la instancia.
            this.estadoMesaId = 1; // Se inicializa en 1 "Cliente Esperando Pedido"
        }

        private int id;
        private string codigo;
        private int estadoMesaId;
        private EstadoMesa estadoMesa;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required, StringLength(5, MinimumLength = 5)]
        public string Codigo
        {
            get => codigo;
            private set => codigo = value; // Solo el constructor pueda establecer el valor.

            //get => codigo; 
            //set
            //{
            //    if (value.Length != 5)
            //    {
            //        throw new ArgumentException("El código de la mesa debe tener exactamente 5 caracteres.");
            //    }
            //    codigo = value;
            //}
        }
        [Required]
        public int EstadoMesaId { get => estadoMesaId; set => estadoMesaId = value; } // FK para EstadoMesa
        public EstadoMesa EstadoMesa { get => estadoMesa; set => estadoMesa = value; }

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
