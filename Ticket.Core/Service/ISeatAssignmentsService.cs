using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Service
{
    public interface ISeatAssignmentsService
    {
        void AssignSeats(int eventId);
    }
}
