using Ticket.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<SeatAssignments> SeatAssignments { get; set; }
        public DbSet<Hole> Hole { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
