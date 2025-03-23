using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;

namespace Ticket.Core.Service
{
    public interface ITicketsService
    {
        public void AddTickets(Tickets t);

    }
}
