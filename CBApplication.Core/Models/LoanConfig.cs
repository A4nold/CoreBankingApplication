using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class LoanConfig
    {
        public int Id { get; set; }

        [Display(Name = "Debit Interest Rate")]
        public decimal DebitInterestRate { get; set; }

        [Display(Name = "Loan Interest GlAccount")]
        public GlAccountManagement GlAccount { get; set; }

        public int LoanInterestReceivableGlId { get; set; }

        [Display(Name = "Loan Interest Gl Account")]
        public int IncomeGlAccountId { get; set; }

        public bool Status { get; set; }
    }
}
