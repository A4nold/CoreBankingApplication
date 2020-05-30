using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class LoanConfigurationViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Debit Interest Rate")]
        public decimal DebitInterestRate { get; set; }

        [Display(Name = "Loan Interest GlAccount")]
        public GlAccountManagement GlAccount { get; set; }

        [Display(Name = "Loan Interest Income Gl Account")]
        public int IncomeGlAccountId { get; set; }

        [Display(Name = "Loan Interest Receivable Gl Account")]
        public int LoanInterestReceivableGlId { get; set; }

        [Display(Name = "Income Accounts")]
        public IEnumerable<GlAccountManagement> IncomeAccount { get; set; }

        public IEnumerable<GlAccountManagement> RecievableIncomeAccount { get; set; }

        public LoanConfigurationViewModels(LoanConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                DebitInterestRate = config.DebitInterestRate;
                GlAccount = config.GlAccount;
                IncomeGlAccountId = config.IncomeGlAccountId;
                LoanInterestReceivableGlId = config.LoanInterestReceivableGlId;
            }
        }
    }
}
