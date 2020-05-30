using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class TrialBalanceViewModel
    {
       public IEnumerable<TransactionsLog> TransactionLogs { get; set; }

       public string Category { get; set; }

       public string AcctName { get; set; }

       public long AccountCode { get; set; }

       public decimal totalDebit { get; set; }

       public decimal totalCredit { get; set; }
    }
}
