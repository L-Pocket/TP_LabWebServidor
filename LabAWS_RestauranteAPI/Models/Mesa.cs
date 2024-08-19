using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("mesas")]
public partial class Mesa
{
    [Key]
    [Column("id_mesa")]
    public int IdMesa { get; set; }

    [Column("nombre")]
    [StringLength(5)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("id_estado")]
    public int? IdEstado { get; set; }

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<Comanda> Comanda { get; set; } = new List<Comanda>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Mesas")]
    public virtual EstadosMesa? IdEstadoNavigation { get; set; }
}
