using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(int? id);

        User GetbyEmail(string username);

        void Add(User entity);

        User GetByUsernameAndPassword(string email, string password);

        User GetIdInDb(int id);

        void Update(User entity);

        int NumberOfTellers();

        int NumberOfAdmins();
        //User Entry(int id);
    }
}
