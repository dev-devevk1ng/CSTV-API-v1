/*
    Date 4 Mar 2026
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.Models.Player
{
    [Table("Player", Schema = "Player")]
    public class PlayerModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public required string Nickname { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
