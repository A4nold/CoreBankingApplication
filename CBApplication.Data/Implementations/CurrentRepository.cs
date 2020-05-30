using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class CurrentRepository:Repository<CurrentConfig>, ICurrentRepository
    {
        private readonly ApplicationDbContext _context;

        public CurrentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public CurrentConfig GetCurrentConfig()
        {
            var result = _context.CurrentAccount.FirstOrDefault();
            return result;
        }

        public CurrentConfig GetCurrent(int? id)
        {
            var result = _context.Set<CurrentConfig>().Find(id);
            return result;
        }

        public string CheckConfigurations()
        {
            var result = "";

            var check = _context.CurrentAccount.FirstOrDefault();

            if (check != null)
            {
                result = "Success";
            }

            return result;
        }
    }
}
