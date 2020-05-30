using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class BankConfigRepository:Repository<BankConfig>, IBankConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public BankConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public BankConfig GetIsOpenFalse()
        {
            var result = _context.BankConfigs.FirstOrDefault(c => c.IsOpen == false);
            return result;
        }

        public BankConfig GetIsOpenTrue()
        {
            var result = _context.BankConfigs.FirstOrDefault(c => c.IsOpen == true);
            return result;
        }

    }
}
