using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class ProfitandLossViewModels
    {
        public IEnumerable<GlAccountManagement> IncomeGL { get; set; }

        public IEnumerable<GlAccountManagement> ExpenseGl { get; set; }

        public decimal totalIncome { get; set; }

        public decimal totalExpense { get; set; }

        public decimal profit { get; set; }
    }
}
