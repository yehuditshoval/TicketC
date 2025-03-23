using Clean.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;
using Ticket.Core.Repositories;

namespace Ticket.Data.Repositories
{
    public class EventRepository: IEventRepository
    {
        private readonly DataContext _dataContext;

        public EventRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Event> GetList()
        {
            //return _dataContext.Event.Include(u => u.Plan).ToList();
            return _dataContext.Event.ToList();

        }

        public Event GetEventById(int id)
        {
            return _dataContext.Event.Find(id);
        }

        public void DeleteEvent(int id)
        {
            var c = _dataContext.Event.Find(id);
            _dataContext.Remove(c);
            _dataContext.SaveChanges();

        }
        public void UpdateEvent(int id, Event e)
        {
            var c = _dataContext.Event.Find(id);
            if (c != null)
            {
                e.NumTicketExist = e.NumTicketExist;
                e.NumTicket=e.NumTicket;
                e.Date=e.Date;
                e.Name=e.Name;


                _dataContext.Event.Update(c);
                _dataContext.SaveChanges();
            }

        }
        public void AddEvent(Event e)
        {
            _dataContext.Event.Add(e);
            _dataContext.SaveChanges();
        }

    }
}
