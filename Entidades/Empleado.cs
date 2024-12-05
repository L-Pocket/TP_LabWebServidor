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
        public Empleado()
        {
            this.EmpleadoActivo = true; // Se inicializa activo = true
        }

        private int id;
        private string nombre;
        private string usuario;
        private string password;
        private int sectorId;
        private Sector sector;
        private int rolId;
        private Rol rol;
        private bool empleadoActivo;

        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

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
        public string Usuario
        {
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
        public string Password
        {
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
        public int SectorId { get => sectorId; set => sectorId = value; } // FK para Sector
        public virtual Sector Sector { get => sector; set => sector = value; }

        [Required]
        public int RolId { get => rolId; set => rolId = value; } // FK para Rol      
        public virtual Rol Rol { get => rol; set => rol = value; }

        [Required]
        public bool EmpleadoActivo { get => empleadoActivo; set => empleadoActivo = value; } //Posibilidad de suspender empleados. Suspendido = False



    }
}
