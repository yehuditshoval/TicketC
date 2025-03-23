using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.DTOs;
using Ticket.Core.Models;

namespace Ticket.Service.Mappings
{
    public class EventMapping
    {

        public static EventSummaryDto ToEventSummaryDto(Event e)
        {
            return new EventSummaryDto
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date
            };
        }

    }
}
