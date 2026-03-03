using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CSTV_v1.Models.Player;

[Table("AlternateID", Schema = "Player")]
public partial class AlternateId
{
    [Key]
    public int Id { get; set; }

    public int PlayerId { get; set; }

    [StringLength(40)]
    public string Nickname { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("AlternateIds")]
    public virtual Player Player { get; set; } = null!;
}
