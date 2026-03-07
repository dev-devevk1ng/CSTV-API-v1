/*
    Date 4 Mar 2026
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSTV_v1.Models.Player
{
    [Table("Profile", Schema = "Player")] // <- schema Player
    public class ProfileModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid PlayerId { get; set; } // FK para Player

        [Required, MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(30)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime Born { get; set; }

        [Required, MaxLength(15)]
        public string Status { get; set; } = null!; // Active, Benched, Retired

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal ApproxTotalWinnings { get; set; }

        [Required]
        public int YearCareerStart { get; set; }

        public int? YearCareerEnd { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navegação para Player
        [ForeignKey("PlayerId")]
        public PlayerModel Player { get; set; } = null!;
    }
}