using Microsoft.AspNetCore.Mvc;
using Ticket.Core.Service;
using Ticket.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatAssignmentsController : ControllerBase {
                private readonly ISeatAssignmentsService _SeatAssignmentsService;
    public SeatAssignmentsController(ISeatAssignmentsService SeatAssignmentsService)
    {
        _SeatAssignmentsService = SeatAssignmentsService;
    }

   
        // GET: api/<SeatAssignmentsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SeatAssignmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SeatAssignmentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SeatAssignmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SeatAssignmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
