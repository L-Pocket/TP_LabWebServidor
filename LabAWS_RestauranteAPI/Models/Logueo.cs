using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("logueos")]
public partial class Logueo
{
    [Key]
    [Column("id_logueo")]
    public int IdLogueo { get; set; }

    [Column("id_empleado")]
    public int? IdEmpleado { get; set; }

    [Column("fecha_logueo", TypeName = "datetime")]
    public DateTime FechaLogueo { get; set; }

    [Column("fecha_deslogueo", TypeName = "datetime")]
    public DateTime? FechaDeslogueo { get; set; }

    [ForeignKey("IdEmpleado")]
    [InverseProperty("Logueos")]
    public virtual Empleado? IdEmpleadoNavigation { get; set; }
}
