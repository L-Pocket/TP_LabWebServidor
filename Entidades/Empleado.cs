using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Empleado
    {
        private int idEmpleado;
        private string nombre;
        private string usuario;
        private string password;
        private int sectorDelEmpleadoId;
        private Sector sectorDelEmpleado;
        private int rolDelEmpleadoId;
        private Rol rolDelEmpleado;
        private bool empleadoActivo;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }

        [Required, MaxLength(100)]
        public string Nombre 
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre no puede estar vacío.");
                }
                nombre = value;
            }
        }

        [Required, MaxLength(50)]
        public string Usuario {
            get => usuario;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El usuario no puede estar vacío.");
                }
                usuario = value;
            }
        }

        [Required, MaxLength(50)]
        public string Password {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La contraseña no puede estar vacía.");
                }
                password = value;
            }
        }

        [Required]
        public int SectorDelEmpleadoId { get => sectorDelEmpleadoId; set => sectorDelEmpleadoId = value; } // FK para Sector
        public virtual Sector SectorDelEmpleado { get => sectorDelEmpleado; set => sectorDelEmpleado = value; }

        [Required]
        public int RolDelEmpleadoId { get => rolDelEmpleadoId; set => rolDelEmpleadoId = value; } // FK para Rol      
        public virtual Rol RolDelEmpleado { get => rolDelEmpleado; set => rolDelEmpleado = value; }

        [Required]
        public bool EmpleadoActivo { get => empleadoActivo; set => empleadoActivo = value; } //Posibilidad de suspender empleados. Suspendido = False


        // -----------------------------------------------------
        // Código viejo:
        //[Key, Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int IdEmpleado { get; set; }
        //[Required, MaxLength(100)]
        //public string Nombre { get; set; }
        //[Required, MaxLength(50)]
        //public string Usuario { get; set; }
        //[Required, MaxLength(50)]
        //public string Password { get; set;}
        //[Required]
        //public int SectorDelEmpleadoId { get; set; } // FK para Sector
        //public virtual Sector SectorDelEmpleado { get; set; } 
        //[Required]
        //public int RolDelEmpleadoId { get; set; } // FK para Rol        
        //public virtual Rol RolDelEmpleado { get; set; } 
        //[Required]
        //public bool EmpleadoActivo { get; set; } //Posibilidad de suspender empleados. Suspendido = False
    }
}
