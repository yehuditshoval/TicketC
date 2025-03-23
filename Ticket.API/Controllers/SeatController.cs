using Microsoft.AspNetCore.Mvc;
using Ticket.Core.Models;
using Ticket.Core.Service;
using Ticket.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _SeatService;
        public SeatController(ISeatService SeatService)
        {
            _SeatService = SeatService;
        }

        // GET: api/<SeatController>
        [HttpGet]
        public IEnumerable<Seat> Get()
        {
            return _SeatService.GetAll();
        }

        // GET api/<SeatController>/5
        [HttpGet("{id}")]
        public Seat GetSeat(int id)
        {
            return _SeatService.GetSeatById(id);
        }

        // POST api/<SeatController>
        [HttpPost]
        public IActionResult AddSeat([FromBody] Seat s)
        {
            if (s == null)
            {
                return BadRequest("Seat data is required.");
            }

            _SeatService.AddSeat(s);
            return Ok("Seat added successfully!");
        }

        // PUT api/<SeatController>/5
        [HttpPut("{id}")]
        public void Put(int id, Seat s)
        {
            _SeatService.UpdateSeat(id, s);
        }

        // DELETE api/<SeatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _SeatService.DeleteSeat(id);
        }
    }
}
