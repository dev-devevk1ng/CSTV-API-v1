/*
    17 Mar 2026
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSLA.DTO.Player
{
    public class PlayerResponseDTO
    {
        public Guid Id { get; set; }
        public required string Nickname { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}