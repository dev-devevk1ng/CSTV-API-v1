using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CSTV_v1.Models.Player;

[Table("Roles", Schema = "Player")]
public partial class Role
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string Roles { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
