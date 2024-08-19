using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("productos")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_sector")]
    public int? IdSector { get; set; }

    [Column("descripcion")]
    [StringLength(255)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("stock")]
    public int Stock { get; set; }

    [Column("precio", TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    [ForeignKey("IdSector")]
    [InverseProperty("Productos")]
    public virtual Sectore? IdSectorNavigation { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
