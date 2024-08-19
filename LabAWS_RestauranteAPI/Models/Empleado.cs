using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("empleados")]
public partial class Empleado
{
    [Key]
    [Column("id_empleado")]
    public int IdEmpleado { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("usuario")]
    [StringLength(100)]
    [Unicode(false)]
    public string Usuario { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("id_sector")]
    public int? IdSector { get; set; }

    [Column("id_rol")]
    public int? IdRol { get; set; }

    [ForeignKey("IdRol")]
    [InverseProperty("Empleados")]
    public virtual Role? IdRolNavigation { get; set; }

    [ForeignKey("IdSector")]
    [InverseProperty("Empleados")]
    public virtual Sectore? IdSectorNavigation { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Logueo> Logueos { get; set; } = new List<Logueo>();
}
