/*
    Date 25 Mar 2026
    Gaules
        Sons of The Forest Ep.2
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.Models.Player
{
    [Table("AlternateID", Schema = "Player")]
    public class AlternateIDModel
    {
        [Key]
        public int Id { get; set; }

        public Guid PlayerId { get; set; }

        [Required]
        [StringLength(40)]
        public required string AlternateID { get; set; }

        //🔹 Relacionamento 1:1, Navegação
        [ForeignKey("PlayerId")]
        public PlayerModel? Player { get; set; }
    }
}

