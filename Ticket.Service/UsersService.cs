using Ticket.Core.Models;
using Ticket.Core.Repositories;
using Ticket.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void AddUser(Users user)
        {
            _usersRepository.AddUser(user);
        }


        public Users GetUsersById(int id)
        {
            return _usersRepository.GetUsersById(id);
        }

        public List<Users> GetAll()
        {
            return _usersRepository.GetList();
        }

        public void DeleteUsers(int id)
        {
            _usersRepository.DeleteUsers(id);
        }

        public void UpdateUsers(int id, Users b)
        {
            _usersRepository.UpdateUsers(id,b);
        }
    }
}
