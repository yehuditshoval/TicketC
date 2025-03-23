using Clean.Data;
using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Models;
using Ticket.Core.Repositories;

namespace Ticket.Data.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly DataContext _dataContext;

        public TicketsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Tickets> GetTicketsByEvent(int eventId)
        {
            return _dataContext.Tickets
                .Where(ticket => ticket.EventId == eventId)
                .ToList();
        }
        public void AddTickets(Tickets t)
        {
            _dataContext.Tickets.Add(t);
            _dataContext.SaveChanges();
        }
    }
}
