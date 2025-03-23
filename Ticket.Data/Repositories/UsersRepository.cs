using Ticket.Core.Models;
using Ticket.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Data;

namespace Ticket.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _dataContext;

        public UsersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Users GetUsersById(int id)
        {
            return _dataContext.Users.Find(id);
        }


        public List<Users> GetList()
        {
            //return _dataContext.Users.Include(u => u.Plan).ToList();
            return _dataContext.Users.ToList();
        }


        public void DeleteUsers(int id)
        {
            var user = _dataContext.Users.Find(id);
            _dataContext.Remove(user);
            _dataContext.SaveChanges();

        }

        public void UpdateUsers(int id, Users b)
        {
            var user = _dataContext.Users.Find(id);
            if (user != null)
            {
                user.Name = b.Name; // עדכן שדות רלוונטיים
                user.Email = b.Email;

                _dataContext.Users.Update(user);
                _dataContext.SaveChanges();
            }

        }

        public void AddUser(Users user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }

        public Users GetUserByUsername(string username)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Name == username);
        }

    }
}
