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
    public class EventService: IEventService
    {
        private readonly IEventRepository _EventRepository;
        public EventService(IEventRepository EventRepository)
        {
            _EventRepository = EventRepository;
        }

        public Event GetEventById(int id)
        {
            return _EventRepository.GetEventById(id);
        }

        public List<Event> GetAll()
        {
            return _EventRepository.GetList();
        }

        public void DeleteEvent(int id)
        {
            _EventRepository.DeleteEvent(id);
        }

        public void UpdateEvent(int id, Event e)
        {
            _EventRepository.UpdateEvent(id,e);
        }

        public void AddEvent(Event e)
        {
            _EventRepository.AddEvent(e);
        }
    }
}
