/*
    Date 4 Mar 2026
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CSLA.Models.Player
{
    [Table("Profile", Schema = "Player")]
    public class ProfileModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        [StringLength(30)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public required string LastName { get; set; }

        [Required]
        public DateOnly Born { get; set; }

        [Required]
        [StringLength(15)]
        public required string Status { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ApproxTotalWinnings { get; set; }

        [Required]
        public int YearCareerStart { get; set; }

        public int? YearCareerEnd { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //🔹 Relacionamento 1:1, Navegação
        [ForeignKey("PlayerId")]
        public PlayerModel? Player { get; set; }
    }
}