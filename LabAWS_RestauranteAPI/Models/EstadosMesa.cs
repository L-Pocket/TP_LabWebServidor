using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("estados_mesas")]
public partial class EstadosMesa
{
    [Key]
    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();
}
