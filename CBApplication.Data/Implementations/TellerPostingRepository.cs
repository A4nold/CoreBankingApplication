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
    public class TellerPostingRepository : Repository<TellerPosting>, ITellerPostingRepository
    {
        private readonly ApplicationDbContext _context;
        public TellerPostingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<TellerPosting> GetTellerPosting(int? id)
        {
            var result = _context.TellerPostings.Where(a => a.UserId == id).Include(a => a.Customer);
            return result;
        }

        public TellerManagement GetTillUserId(int? id)
        {
            var result = _context.TellerManagements.FirstOrDefault(a => a.userId == id);
            return result;
        }

        public int NumberOfTillAccounts()
        {
            var count = _context.TellerManagements.Count();
            return count;
        }
    }
}
