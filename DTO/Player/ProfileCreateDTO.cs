/*
    Date 9 Mar 2026
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.DTO.Player
{
    public class ProfileCreateDTO
    {
        public required Guid PlayerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly Born { get; set; }
        public required string Status { get; set; }
        public decimal ApproxTotalWinnings { get; set; }
        public required int YearCareerStart { get; set; }
        public int? YearCareerEnd { get; set; }
    }
}