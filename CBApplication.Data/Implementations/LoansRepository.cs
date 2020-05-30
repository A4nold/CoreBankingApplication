using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class LoansRepository : Repository<Loans>,ILoansRepository
    {
        private readonly ApplicationDbContext _context;

        public LoansRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Loans> GetActiveLoan()
        {
            return _context.Loans.Where(m => m.Status == true);
        }

        public bool CheckIfPaid(long accountNumber)
        {
            var isLoanComplete = _context.Loans.Where(c => c.Status == true && c.CustomerAccountNumber == accountNumber).FirstOrDefault();

            if (isLoanComplete != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Loans LoanAccountExist(long accountNumber)
        {
            var isLoanComplete = _context.Loans.Where(c => c.CustomerAccountNumber == accountNumber).FirstOrDefault();

            if (isLoanComplete!=null)
            {
                return isLoanComplete;
            }
            else
            {
                return null;
            }
        }

        public Loans GetCurrentLoan(long accountNumber)
        {
            var check = _context.Loans.Where(m => m.AccountNumber == accountNumber).FirstOrDefault();
            return check;
        }

        public int NumberOfLoanAccounts()
        {
            var loan = _context.Loans.Count();
            return loan;
        }

        public Loans GetCurrentLoanById(int id)
        {
            return _context.Loans.Where(m => m.Id == id).FirstOrDefault();
        }
    }
}
