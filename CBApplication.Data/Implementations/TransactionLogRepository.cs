using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Data.Implementations
{
    public class TransactionLogRepository : Repository<TransactionsLog>
    {
        private readonly ApplicationDbContext _context;
        public TransactionLogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<TransactionsLog> GetAllLogs()
        {
            var result = _context.Transactions.ToList();
            return result;
        }

        public List<TransactionsLog> GetDebitLogs()
        {
            return _context.Transactions.Where(a => a.Type == TransactionType.Debit).ToList();
        }
    }
}
