using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;

namespace Ticket.Core.Repositories
{
    public interface ITicketsRepository
    {
        List<Tickets> GetTicketsByEvent(int eventId);
        public void AddTickets(Tickets t);

    }

}
