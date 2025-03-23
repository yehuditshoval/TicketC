using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;
using Ticket.Core.Repositories;
using Ticket.Core.Service;

namespace Ticket.Service
{
    public class TicketsService: ITicketsService
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }
        public void AddTickets(Tickets t)
        {
            _ticketsRepository.AddTickets(t);
        }


    }
}
