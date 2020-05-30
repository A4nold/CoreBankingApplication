using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class CurrentConfig
    {
        public int Id { get; set; }

        
        [Display(Name = "Credit Interest Rate")]
        public decimal CreditInterestRate { get; set; }

        
        [Display(Name = "Minimum Balance")]
        public decimal MinBalance { get; set; }

        [Display(Name = "Current Interest GlAccount")]
        public GlAccountManagement GlAccount { get; set; }

        
        [Display(Name = "Current Gl Account")]
        public int ExpenseGlAccountId { get; set; }

        [Display(Name = "Commission On Turnover")]
        public decimal CommissionOnTurnover { get; set; }

        [Display(Name = "Commission On Turnover Gl Account")]
        public GlAccountManagement CotGlAccount { get; set; }

        [Display(Name = "Income Gl Account")]
        public int IncomeGlAccountId { get; set; }

        public bool status { get; set; }

        public int CurrentInterestPayableGlId { get; set; }
    }
}
