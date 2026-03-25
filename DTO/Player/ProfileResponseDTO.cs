using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLA.DTO.Player
{
    public class ProfileResponseDTO
    {
        public int Id { get; set; }
        public Guid PlayerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly Born { get; set; }
        public required string Status { get; set; }
        public decimal ApproxTotalWinnings { get; set; }
        public int YearCareerStart { get; set; }
        public int? YearCareerEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string PlayerNickname { get; set; }
    }
}