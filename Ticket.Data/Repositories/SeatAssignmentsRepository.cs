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
    public class SeatAssignmentsRepository : ISeatAssignmentsRepository
    {
        private readonly DataContext _dataContext;

        public SeatAssignmentsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(SeatAssignments seatAssignment)
        {
            _dataContext.SeatAssignments.Add(seatAssignment);
            _dataContext.SaveChanges();
        }
    }

}
