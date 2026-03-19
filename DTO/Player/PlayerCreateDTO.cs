/*
    4 Mar 2026
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.DTO.Player
{
    public class PlayerCreateDTO
    {
        [Required]
        [MaxLength(40)]
        public required string Nickname { get; set; }     
    }
}