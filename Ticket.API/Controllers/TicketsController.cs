using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ticket.Core.Models;
using Ticket.Core.Service;
using Ticket.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _TicketsService;
        private readonly IEventService _EventService;
        private readonly ISeatAssignmentsService _SeatAssignmentsService;

        public TicketsController(ITicketsService TicketsService, IEventService EventService, ISeatAssignmentsService seatAssignmentsService)
        {
            _TicketsService = TicketsService;
            _EventService = EventService;
            _SeatAssignmentsService = seatAssignmentsService;
        }

        // GET: api/<TicketsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TicketsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketsController>
        [HttpPost]
        public IActionResult AddTicket([FromBody] Tickets ticket)
        {
            if (ticket == null)
            {
                return BadRequest(new { message = "Ticket data is required." });
            }

            try
            {
                Event eventDetails = _EventService.GetEventById(ticket.EventId);
                if (eventDetails == null)
                {
                    return NotFound(new { message = "Event not found." });
                }

                if (eventDetails.NumTicketExist <= 0)
                {
                    return BadRequest(new { message = "נגמרו הכרטיסים!" });
                }

                if (ticket.NumOfTickets > eventDetails.NumTicketExist)
                {
                    return BadRequest(new { message = "אין מספיק כרטיסים, קנה פחות!" });
                }


                // הוספת הכרטיס
                _TicketsService.AddTickets(ticket);



                eventDetails.NumTicketExist -= ticket.NumOfTickets;
                _EventService.UpdateEvent(eventDetails.Id, eventDetails);

                // אם כל הכרטיסים נמכרו, מפעילים את השיבוץ
                if (eventDetails.NumTicketExist == 0)
                {
                    _SeatAssignmentsService.AssignSeats(ticket.EventId);
                }

                return Ok(new { message = "Ticket added successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error adding ticket: {ex.Message}" });
            }
        }



        // PUT api/<TicketsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
