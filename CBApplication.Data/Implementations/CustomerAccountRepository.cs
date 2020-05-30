using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;
using System.Data.Entity;

namespace CBApplication.Data.Implementations
{
    public class CustomerAccountRepository:Repository<CustomerAccount>, ICustomerAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<CustomerAccount> Find(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerAccount> GetActiveAccounts()
        {
            var result = _context.CustomerAccounts
                .Include(m => m.Branches)
                .Where(m => m.isClosed == false).ToList();

            return result;
        }

        public IEnumerable<CustomerAccount> GetAccountType()
        {
            var result = _context.CustomerAccounts
                .Where(m => m.AccountType == AccountType.Savings)
                .Where(m => m.AccountType == AccountType.Current);

            return result;
        }

        public IEnumerable<CustomerAccount> GetByAccountType(AccountType type)
        {
            var result = _context.CustomerAccounts.Where(m => m.AccountType == type).ToList();

            return result;
        }

        public IEnumerable<CustomerAccount> GetLoanAccounts()
        {
            var result = _context.CustomerAccounts.Where(m => m.AccountType == AccountType.Loan);
            return result;
        }

        public CustomerAccount GetByAccountNumber(long accountNumber)
        {
            var result = _context.CustomerAccounts.
                FirstOrDefault(m => m.AccountNumber == accountNumber);
            return result;
        }

        public IEnumerable<CustomerAccount> GetCurrentAccount(int id)
        {
            var result = _context.CustomerAccounts.Where(m => m.Id == id)
                .Where(m => m.AccountType == AccountType.Current);
            return result;
        }

        public int NumberOfCustomerAccounts()
        {
            var customer = _context.CustomerAccounts.Count(m => m.AccountType == AccountType.Current || m.AccountType == AccountType.Savings);
            return customer;
        }
    }
}
