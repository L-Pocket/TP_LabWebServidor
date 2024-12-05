using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class LogueoEmpleado
    {

        private int id;
        private int empleadoLogId;
        private Empleado empleadoLog;
        private DateTime fechaLogueo;
        private DateTime? fechaDeslogueo;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        public int EmpleadoLogId { get => empleadoLogId; set => empleadoLogId = value; } // FK para Empleado
        public Empleado EmpleadoLog { get => empleadoLog; set => empleadoLog = value; }

        [Required]
        public DateTime FechaLogueo
        {
            get => fechaLogueo;
            set
            {
                if (value == default)
                {
                    throw new ArgumentException("La fecha de logueo no puede ser la fecha predeterminada.");
                }
                fechaLogueo = value;
            }
        }
        public DateTime? FechaDeslogueo
        {
            get => fechaDeslogueo;
            set
            {
                if (value.HasValue && fechaLogueo > value.Value)
                {
                    throw new ArgumentException("La fecha de deslogueo no puede ser anterior a la fecha de logueo.");
                }
                fechaDeslogueo = value;
            }
        }


    }
}
