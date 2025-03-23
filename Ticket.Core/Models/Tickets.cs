using System;
using System.Collections.Generic;

namespace Ticket.Core.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int NumOfTickets { get; set; }

        // העדפות של המשתמש
        public bool NeedsAccessibleSeat { get; set; } // האם זקוק למקום נגיש
        public bool PrefersCloserToStage { get; set; } // האם מעדיף מקום קרוב לבמה
        public bool PrefersCenter { get; set; } // האם מעדיף לשבת במרכז
        public bool PrefersAisleSeat { get; set; } // האם מעדיף מושב ליד המעבר
        public int PreferredRow { get; set; } // העדפה לשורה
    }

}
