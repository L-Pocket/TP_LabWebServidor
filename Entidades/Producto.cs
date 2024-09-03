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
        private int idProducto;
        private int sectorProductoId;
        private Sector? sectorProducto;
        private string nombreDescProducto;
        private int stockProducto;
        private decimal precioProducto;
        private int empleadoId;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get => idProducto; set => idProducto = value; }

        [Required]
        public int SectorProductoId { get => sectorProductoId; set => sectorProductoId = value; }  // FK de Sector
        public Sector? SectorProducto { get => sectorProducto; set => sectorProducto = value; }

        [Required]
        public string NombreDescProducto
        {
            get => nombreDescProducto;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre/descripción del producto no puede estar vacío.");
                }
                nombreDescProducto = value;
            }
        }

        [Required]
        public int StockProducto
        {
            get => stockProducto;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El stock del producto no puede ser negativo.");
                }
                stockProducto = value;
            }
        }

        [Required]
        public decimal PrecioProducto
        {
            get => precioProducto;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("El precio del producto no puede ser negativo.");
                }
                precioProducto = value;
            }
        }        

        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdProducto { get; set; }
        //[Required]
        //public int SectorProductoId { get; set; } // FK de Sector
        //public virtual Sector? SectorProducto { get; set; } 
        //[Required]
        //public string NombreDescProducto { get; set; }
        //[Required]
        //public int StockProducto { get; set; }
        //[Required]
        //public decimal PrecioProducto { get; set;}
        
    }
}
