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
    public class GlPostingRepository : Repository<GlPosting>, IGlPostingRepository
    {
        private readonly ApplicationDbContext _context;

        public GlPostingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<GlPosting> GetAllPosting()
        {
            var result = _context.GlPosting.Include(m => m.User)
                .Include(m => m.DebitAccount)
                .Include(m => m.CreditAccount);
            return result;
        }
    }
}
