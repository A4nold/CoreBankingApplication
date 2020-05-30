using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    interface ITellerRepository : IRepository<TellerManagement>
    {
        TellerManagement Get(int? id);

        IEnumerable<TellerManagement> GetAll();

        IEnumerable<TellerManagement> GetAllUsersAndAccount();

        TellerManagement GetUserId(int? id);

    }
}
