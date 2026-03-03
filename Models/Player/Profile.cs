using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CSTV_v1.Models.Player;

[Table("Profile", Schema = "Player")]
[Index("PlayerId", Name = "UQ__Profile__4A4E74C93CFF67AA", IsUnique = true)]
public partial class Profile
{
    [Key]
    public int Id { get; set; }

    public int PlayerId { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    public string LastName { get; set; } = null!;

    public DateOnly Born { get; set; }

    [StringLength(15)]
    public string Status { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ApproxTotalWinnings { get; set; }

    public int YearCareerStart { get; set; }

    public int? YearCareerEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("PlayerId")]
    [InverseProperty("Profile")]
    public virtual Player Player { get; set; } = null!;
}
