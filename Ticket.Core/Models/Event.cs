using System;

namespace Ticket.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }  // המיקום של האירוע
        public string Description { get; set; }  // תיאור של האירוע
        public int HoleId { get; set; }
        public int NumTicket { get; set; }  // מספר כרטיסים עבור האירוע
        public int NumTicketExist { get; set; }  // מספר כרטיסים שנותרו
        public int Price { get; set; }
    }

}
