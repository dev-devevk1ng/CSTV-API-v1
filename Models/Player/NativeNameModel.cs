/*
    Date 24 Mar 2026
    Gaules
        Sons of The Forest Ep.1
*/

using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.Models.Player
{
    [Table("NativeName", Schema = "Player")]
    public class NativeNameModel
    {
        [Key]
        [Required]
        public Guid PlayerId { get; set; }

        [MaxLength(30)]
        [Required]
        public required string NativeFirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public required string NativeLastName { get; set; }

        //🔹 Relacionamento 1:1, Navegação
        [ForeignKey("PlayerId")]
        public PlayerModel? Player { get; set; }
    }
}