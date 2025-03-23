using Ticket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Service
{
    public interface IUsersService
    {
        public List<Users> GetAll();
        public void DeleteUsers(int id);
        public void UpdateUsers(int id, Users b);
        public Users GetUsersById(int id);
        public void AddUser(Users user);
    }
}
