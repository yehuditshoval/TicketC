using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class Seat
    {
        public int Id { get; set; } 
        public int EventId { get; set; }
        public int numLine { get; set; }
        public int SeatNumber { get; set; } 
        public bool IsAvailable { get; set; } 
        public bool IsAccessible { get; set; } 
    }
}

