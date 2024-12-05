using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Producto
    {

        private int id;
        private int sectorId;
        private Sector? sector;
        private string nombreDesc;
        private int stock;
        private decimal precio;


        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        public int SectorId { get => sectorId; set => sectorId = value; }  // FK de Sector
        public Sector? Sector { get => sector; set => sector = value; }

        [Required]
        public string NombreDesc
        {
            get => nombreDesc;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre/descripción del producto no puede estar vacío.");
                }
                nombreDesc = value;
            }
        }

        [Required]
        public int Stock
        {
            get => stock;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El stock del producto no puede ser negativo.");
                }
                stock = value;
            }
        }

        [Required]
        public decimal Precio
        {
            get => precio;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El precio del producto no puede ser negativo.");
                }
                precio = value;
            }
        }

        public void ReducirStock(int cantidad)
        {
            int nuevoStock = this.Stock - cantidad;
            if (nuevoStock < 0)
            {
                throw new ArgumentException($"No se puede reducir el stock ya que el stock actual es {this.Stock}");
            }
            this.Stock = nuevoStock;
        }



    }
}
