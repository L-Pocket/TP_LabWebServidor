using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("sectores")]
public partial class Sectore
{
    [Key]
    [Column("id_sector")]
    public int IdSector { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdSectorNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    [InverseProperty("IdSectorNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
