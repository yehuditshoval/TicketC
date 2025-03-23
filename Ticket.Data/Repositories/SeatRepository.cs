using Clean.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;
using Ticket.Core.Repositories;

namespace Ticket.Data.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DataContext _dataContext;

        public SeatRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Seat> GetAvailableSeats(int eventId)
        {
            // פילטר את המושבים שמיועדים לאירוע ולפני שנכנסו כשיובו
            var availableSeats = _dataContext.Seat
                .Where(seat => seat.EventId == eventId && seat.IsAvailable == true)
                .ToList();

            return availableSeats;
        }
      
        public List<Seat> GetList()
        {
            //return _dataContext.Seat.Include(u => u.Plan).ToList();
            return _dataContext.Seat.ToList();
        }

        public Seat GetSeatById(int id)
        {
            return _dataContext.Seat.Find(id);
        }

        public void DeleteSeat(int id)
        {
            var s = _dataContext.Seat.Find(id);
            _dataContext.Remove(s);
            _dataContext.SaveChanges();

        }

        public void UpdateSeat(int id, Seat s)
        {
            var existingSeat = _dataContext.Seat.Find(id);
            if (existingSeat != null)
            {
                existingSeat.IsAvailable = s.IsAvailable;
                _dataContext.Seat.Update(existingSeat);
                _dataContext.SaveChanges();
            }
        }
  

        public void AddSeat(Seat s)
        {
            _dataContext.Seat.Add(s);
            _dataContext.SaveChanges();
        }
    }

}
