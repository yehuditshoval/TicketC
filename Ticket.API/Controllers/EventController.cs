using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Core.DTOs;
using Ticket.Core.Models;
using Ticket.Core.Service;
using Ticket.Service;
using Ticket.Service.Mappings;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.API.Controllers
{
    [Route("api/event")]
    [ApiController]
    [Authorize]

    public class EventController : ControllerBase
    {

        private readonly IEventService _EventService;
        public EventController(IEventService EventService)
        {
            _EventService = EventService;
        }


        // GET: api/<EventController>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _EventService.GetAll();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Event GetEvent(int id)
        {
            return _EventService.GetEventById(id);
        }

        // POST api/<EventController>
        [HttpPost]
        public IActionResult AddEvent([FromBody] Event e)
        {
            if (e == null)
            {
                return BadRequest("Event data is required.");
            }

            _EventService.AddEvent(e);
            return Ok("Event added successfully!");
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, Event e)
        {
            _EventService.UpdateEvent(id, e);
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _EventService.DeleteEvent(id);
        }

        [HttpGet("summaries")]
        public ActionResult<IEnumerable<EventSummaryDto>> GetEventSummaries()
        {
            var events = _EventService.GetAll();
            var summaries = events.Select(e => EventMapping.ToEventSummaryDto(e)).ToList();
            return Ok(summaries);
        }

    }
}
