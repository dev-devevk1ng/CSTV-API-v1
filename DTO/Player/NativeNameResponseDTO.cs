/*
    Date 24 Mar 2026
    Gaules
        Sons of The Forest Ep.1
*/

using System;

namespace CSLA.DTO.Player
{
    public class NativeNameResponseDTO
    {
        public required Guid PlayerId { get; set; }
        public required string NativeFirstName { get; set; }
        public required string NativeLastName { get; set; }
        
    }
}