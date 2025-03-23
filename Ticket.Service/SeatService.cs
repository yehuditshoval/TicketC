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
    public class SeatService: ISeatService
    {

        private readonly ISeatRepository _seatRepository;
        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public Seat GetSeatById(int id)
        {
            return _seatRepository.GetSeatById(id);
        }
        public List<Seat> GetAvailableSeats(int eventId)
        {
            return _seatRepository.GetAvailableSeats(eventId);
        }


        public List<Seat> GetAll()
        {
            return _seatRepository.GetList();
        }

        public void DeleteSeat(int id)
        {
            _seatRepository.DeleteSeat(id);
        }

        public void UpdateSeat(int id, Seat s)
        {
            _seatRepository.UpdateSeat(id, s);
        }

        public void AddSeat(Seat s)
        {
            _seatRepository.AddSeat(s);
        }

    }
}
