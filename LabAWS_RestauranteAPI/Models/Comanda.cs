using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.Models;

[Table("comandas")]
public partial class Comanda
{
    [Key]
    [Column("id_comanda")]
    public int IdComanda { get; set; }

    [Column("id_mesa")]
    public int? IdMesa { get; set; }

    [Column("nombre_cliente")]
    [StringLength(255)]
    [Unicode(false)]
    public string NombreCliente { get; set; } = null!;

    [ForeignKey("IdMesa")]
    [InverseProperty("Comanda")]
    public virtual Mesa? IdMesaNavigation { get; set; }

    [InverseProperty("IdComandaNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
