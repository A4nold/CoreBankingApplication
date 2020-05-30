using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    

    public class UserRepository:Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public User GetbyEmail(string username)
        {
            var result = _context.Users.FirstOrDefault(n => n.Username == username);
            return result;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            var result = _context.Users.FirstOrDefault(n => n.Username == username && n.Password == password);
            return result;
        }

        public User GetIdInDb(int id)
        {
            var result = _context.Users.Single(n => n.Id == id);
            return result;
        }

        public int NumberOfTellers()
        {
            var teller = _context.Users.Count(m => m.Role == "Teller");
            return teller;
        }

        public int NumberOfAdmins()
        {
            var admin = _context.Users.Count(m => m.Role == "Admin");
            return admin;
        }

        public IEnumerable<User> GetAllUnassignedUsers()
        {
            var result = _context.Users.Where(u => u.UserAssigned == false).Where(u => u.Role == "Teller").ToList();
            return result;
        }
    }

    
}
