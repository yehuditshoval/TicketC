using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.DTOs
{
    public class EventSummaryDto
    {
        public int Id { get; set; }            
        public string Name { get; set; }
        public DateTime Date { get; set; }

    }
}
