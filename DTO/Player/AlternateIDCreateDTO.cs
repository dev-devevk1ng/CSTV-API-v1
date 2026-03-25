/*
    Date 25 Mar 2026
    Gaules
        Sons of The Forest Ep.5
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSLA.DTO.Player
{
    public class AlternateIDCreateDTO
    {
        public required Guid PlayerId { get; set; }
        public required string AlternateID { get; set; }
    }
}