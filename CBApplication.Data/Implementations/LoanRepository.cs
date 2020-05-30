using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class LoanRepository : Repository<LoanConfig>, ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        

        public LoanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
        public LoanConfig GetLoanConfig()
        {
       
          
            var result = _context.LoanConfig.FirstOrDefault();

            return result;
        }

        public string CheckConfigurations()
        {
            var result = "";

            var check = _context.LoanConfig.FirstOrDefault();

            if (check != null)
            {
                result = "Success";
            }

            return result;
        }

        public LoanConfig GetLoan(int? id)
        {
            var result = _context.Set<LoanConfig>().Find(id);
            return result;
        }
    }
}
