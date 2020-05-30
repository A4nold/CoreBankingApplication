using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class AccountConfigurationRepository:Repository<SavingsConfig>, IAccountConfigurationRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountConfigurationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SavingsConfig> Find(Expression<Func<GlAccountManagement, bool>> predicate)
        {
            throw new NotImplementedException();

        }

        public SavingsConfig GetSavingsConfig()
        {
            var result = _context.SavingsAccount.FirstOrDefault();
            return result;
        }

        public string CheckConfigurations()
        {
            var result = "";

            var check = _context.SavingsAccount.FirstOrDefault();

            if (check != null)
            {
                result = "Success";
            }

            return result;
        }
    }
}
