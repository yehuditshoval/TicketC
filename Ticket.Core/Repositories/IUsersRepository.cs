using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;

namespace Ticket.Core.Repositories
{
    public interface IUsersRepository
    {
        public List<Users> GetList();
        public void DeleteUsers(int id);
        public void UpdateUsers(int id, Users b);
        public Users GetUsersById(int id);
        public void AddUser(Users user);
        public Users GetUserByUsername(string username);

    }
}
