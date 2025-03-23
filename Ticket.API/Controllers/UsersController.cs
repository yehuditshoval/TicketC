using Ticket.Core.Models;
using Ticket.Core.Service;
using Ticket.Service;
using Microsoft.AspNetCore.Mvc;
using Ticket.Core.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _UsersService;
        public UsersController(IUsersService UsersService)
        {
            _UsersService = UsersService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _UsersService.GetAll();
        }
       


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public Users GetUsers(int id)
        {
            return _UsersService.GetUsersById(id);
        }


        // POST api/<UsersController>
        [HttpPost]
        public IActionResult AddUser([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            _UsersService.AddUser(user);
            return Ok("User added successfully!");
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, Users b)
        {
            _UsersService.UpdateUsers(id, b);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _UsersService.DeleteUsers(id);
        }
    }
}
