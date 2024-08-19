using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("pedidos")]
public partial class Pedido
{
    [Key]
    [Column("id_pedido")]
    public int IdPedido { get; set; }

    [Column("id_comanda")]
    public int? IdComanda { get; set; }

    [Column("id_producto")]
    public int? IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("id_estado")]
    public int? IdEstado { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("fecha_finalizacion", TypeName = "datetime")]
    public DateTime? FechaFinalizacion { get; set; }

    [Column("codigo_cliente")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CodigoCliente { get; set; }

    [Column("observaciones")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    [ForeignKey("IdComanda")]
    [InverseProperty("Pedidos")]
    public virtual Comanda? IdComandaNavigation { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("Pedidos")]
    public virtual EstadosPedido? IdEstadoNavigation { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("Pedidos")]
    public virtual Producto? IdProductoNavigation { get; set; }
}
