using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class CurrentConfigurationViewModels
    {
        [Display(Name = "Expense Accounts")]
        public IEnumerable<GlAccountManagement> ExpenseAccount { get; set; }

        [Display(Name = "Income Accounts")]
        public IEnumerable<GlAccountManagement> IncomeAccount { get; set; }

        public IEnumerable<GlAccountManagement> PayableAccount { get; set; }

        public int CurrentInterestPayableGlId { get; set; }

        public int Id { get; set; }


        [Display(Name = "Credit Interest Rate")]
        public decimal CreditInterestRate { get; set; }


        [Display(Name = "Minimum Balance")]
        public decimal MinBalance { get; set; }

        //[Display(Name = "Current Interest GlAccount")]
        //public GlAccountManagement GlAccount { get; set; }


        [Display(Name = "Current Expense Gl Account")]
        public int ExpenseGlAccountId { get; set; }

        [Display(Name = "Commission On Turnover")]
        public decimal CommissionOnTurnover { get; set; }

        [Display(Name = "Commission On Turnover Gl Account")]
        public GlAccountManagement CotGlAccount { get; set; }

        [Display(Name = "Income Gl Account")]
        public int IncomeGlAccountId { get; set; }

        public CurrentConfigurationViewModels(CurrentConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                CreditInterestRate = config.CreditInterestRate;
                MinBalance = config.MinBalance;
                ExpenseGlAccountId = config.ExpenseGlAccountId;
                CommissionOnTurnover = config.CommissionOnTurnover;
                CotGlAccount = config.CotGlAccount;
                IncomeGlAccountId = config.IncomeGlAccountId;
                CurrentInterestPayableGlId = config.CurrentInterestPayableGlId;
            }
        }
    }
}
