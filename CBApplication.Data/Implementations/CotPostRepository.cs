using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class CotPostRepository : Repository<COTPost>, ICotPostRepository
    {
        private readonly ApplicationDbContext _context;

        public CotPostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
