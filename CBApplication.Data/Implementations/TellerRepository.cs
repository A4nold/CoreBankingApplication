using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;
using System.Data.Entity;

namespace CBApplication.Data.Implementations
{
    public class TellerRepository : Repository<TellerManagement>, ITellerRepository
    {
        private readonly ApplicationDbContext _context;

        public TellerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<TellerManagement> GetAllUsersAndAccount()
        {
            return _context.TellerManagements
                .Include(f => f.User).Where(f => f.User.Role != "Admin").Where(f => f.User.UserAssigned)
                .Include(f => f.AccountManagement).ToList();
        }

        public TellerManagement GetUserId(int? id)
        {
            return _context.TellerManagements.FirstOrDefault(m => m.userId == id);
        }
    }
}
