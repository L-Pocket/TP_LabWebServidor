using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Required]
        public int SectorProductoId { get; set; } // FK de Sector
        public virtual Sector? SectorProducto { get; set; } // 

        [Required]
        public string NombreDescProducto { get; set; }

        [Required]
        public int StockProducto { get; set; }

        [Required]
        public decimal PrecioProducto { get; set; }

        public int EmpleadoId { get; set; } // Atributo para luego  listar todos los productos pendientes de este tipo de empleado
    }
}
