using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;

namespace Ticket.Core.Repositories
{
    public interface IEventRepository
    {
        public List<Event> GetList();
        public void DeleteEvent(int id);
        public void UpdateEvent(int id, Event e);
        public Event GetEventById(int id);
        public void AddEvent(Event e);

    }
}
