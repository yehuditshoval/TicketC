using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Models;
using Ticket.Core.Repositories;
using Ticket.Core.Service;

namespace Ticket.Service
{
    public class SeatAssignmentsService : ISeatAssignmentsService
    {
        private readonly ISeatAssignmentsRepository _seatAssignmentsRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;

        public SeatAssignmentsService(ISeatAssignmentsRepository seatAssignmentsRepository, ITicketsRepository ticketsRepository, ISeatRepository seatRepository, IUsersRepository usersRepository, IEmailService emailService)
        {
            _seatAssignmentsRepository = seatAssignmentsRepository;
            _ticketsRepository = ticketsRepository;
            _seatRepository = seatRepository;
            _usersRepository = usersRepository;
            _emailService = emailService;
        }


        public void AssignSeats(int eventId)
        {
            List<Seat> availableSeats = _seatRepository.GetAvailableSeats(eventId);
            List<Tickets> ticketRequests = _ticketsRepository.GetTicketsByEvent(eventId);

            int totalRows = availableSeats.Select(s => s.numLine).Distinct().Count();
            int totalSeatsPerRow = availableSeats.Count / totalRows;

            var costMatrix = new int[ticketRequests.Count, availableSeats.Count];

            for (int i = 0; i < ticketRequests.Count; i++)
            {
                for (int j = 0; j < availableSeats.Count; j++) 
                {
                    var ticket = ticketRequests[i];
                    var seat = availableSeats[j];
                    int cost = 0;

                    if (ticket.NeedsAccessibleSeat && !seat.IsAccessible)
                    {
                        cost = int.MaxValue;
                    }

                    int frontRowsThreshold = Math.Max(1, (int)Math.Ceiling(totalRows * 0.3));
                    if (ticket.PrefersCloserToStage && seat.numLine > frontRowsThreshold)
                    {
                        cost += 5;
                    }

                    int centerStart, centerEnd;

                    if (totalSeatsPerRow <= 3)
                    {
                        centerStart = centerEnd = (totalSeatsPerRow / 2) + 1;
                    }
                    else
                    {
                        centerStart = (int)Math.Floor(totalSeatsPerRow * 0.3) + 1;
                        centerEnd = (int)Math.Ceiling(totalSeatsPerRow * 0.7);
                    }

                    if (ticket.PrefersCenter && (seat.SeatNumber < centerStart || seat.SeatNumber > centerEnd))
                    {
                        cost += 3;
                    }

                    int aisleThreshold = (totalSeatsPerRow <= 4)? 0: (int)Math.Ceiling(totalSeatsPerRow * 0.1);

                    int leftAisleLimit = 1 + aisleThreshold;
                    int rightAisleLimit = totalSeatsPerRow - aisleThreshold;

                    bool isNearAisle = seat.SeatNumber <= leftAisleLimit || seat.SeatNumber >= rightAisleLimit;

                    if (ticket.PrefersAisleSeat && !isNearAisle)
                    {
                        cost += 4;
                    }

                    if (ticket.PreferredRow > 0 && ticket.PreferredRow != seat.numLine)
                    {
                        cost += 2; 
                    }


                    costMatrix[i, j] = cost;
                }
            }

        


        var assignment = HungarianAlgorithmService.Solve(costMatrix);
            var assignedSeats = new List<SeatAssignments>();

            for (int i = 0; i < ticketRequests.Count; i++)
            {
                var seatIndex = assignment[i];
                if (seatIndex != -1)
                {
                    var ticket = ticketRequests[i];
                    var seat = availableSeats[seatIndex];

                    var seatAssignment = new SeatAssignments
                    {
                        EventId = eventId,
                        UserId = ticket.UserId,
                        SeatId = seat.Id
                    };

                    _seatAssignmentsRepository.Add(seatAssignment);
                    seat.IsAvailable = false;
                    _seatRepository.UpdateSeat(seat.Id, seat);
                    assignedSeats.Add(seatAssignment);
                }
            }

            SendSeatAssignmentEmails(assignedSeats);
        }
        private void SendSeatAssignmentEmails(List<SeatAssignments> assignments)
        {
            _emailService.SendSeatAssignmentEmails(assignments);
        }
    }
}









