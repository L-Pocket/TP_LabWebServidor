using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(50)]
        public string Usuario { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public int SectorDelEmpleadoId { get; set; } // FK para Sector
        public virtual Sector SectorDelEmpleado { get; set; } // 

        [Required]
        public int RolDelEmpleadoId { get; set; } // FK para Rol        
        public virtual Rol RolDelEmpleado { get; set; } // 

        [Required]
        public bool EmpleadoActivo { get; set; } //Posibilidad de suspender empleados. Suspendido = False


    }
}