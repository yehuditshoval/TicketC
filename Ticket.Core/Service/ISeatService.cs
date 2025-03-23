using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;

namespace Ticket.Core.Service
{
    public interface ISeatService
    {
        List<Seat> GetAvailableSeats(int eventId);
        public List<Seat> GetAll();
        public void DeleteSeat(int id);
        public void UpdateSeat(int id, Seat s);
        public Seat GetSeatById(int id);
        public void AddSeat(Seat s);
    }
}
