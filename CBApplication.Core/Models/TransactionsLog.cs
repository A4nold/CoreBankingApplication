using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public enum TransactionType
    {
        Credit = 1,
        Debit = 2
    }

    public class TransactionsLog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public long AccountCode { get; set; }

        public DateTime date { get; set; }

        public TransactionType Type { get; set; }

        public string MainAccountCategory { get; set; }
    }
}
