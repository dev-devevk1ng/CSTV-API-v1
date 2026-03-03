using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CSTV_v1.Models.Player;

[Table("Player", Schema = "Player")]
public partial class Player
{
    [Key]
    public int Id { get; set; }

    [StringLength(40)]
    public string Nickname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<AlternateId> AlternateIds { get; set; } = new List<AlternateId>();

    [InverseProperty("Player")]
    public virtual Profile? Profile { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("Players")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
