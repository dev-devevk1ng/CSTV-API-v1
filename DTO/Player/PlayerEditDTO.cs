/*
    6 Mar 2026
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.DTO.Player
{
    public class PlayerEditDTO
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public required string Nickname { get; set; }
    }
}
