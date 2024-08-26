using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LogueoEmpleado
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLogueo { get; set; }

        [Required]
        public int EmpleadoLogId { get; set; } // FK para Empleado
        public virtual Empleado EmpleadoLog { get; set; } // 

        [Required]
        public DateTime FechaLogueo { get; set; }
        public DateTime? FechaDeslogueo { get; set; }   // Puede ser null    

    }
}
