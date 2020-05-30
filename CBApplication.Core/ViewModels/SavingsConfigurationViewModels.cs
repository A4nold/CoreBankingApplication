using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class SavingsConfigurationViewModels
    {
        public IEnumerable<GlAccountManagement> ExpenseAccount { get; set; }

        public IEnumerable<GlAccountManagement> PayableAccount { get; set; }

        public int Id { get; set; }

        //[Display(Name = "Interest GlAccount")]
        //public GlAccountManagement GlAccounts { get; set; }

        
        [Display(Name = "Savings Interest Expense Gl Account")]
        public int GlAccountId { get; set; }

        [Display(Name = "Savings Interest Payable Gl Account")]
        public int SavingsInterestPayableGlId { get; set; }


        [Display(Name = "Credit Interest Rate")]
        public decimal CreditInterestRate { get; set; }

        
        public bool status { get; set; }

        
        [Display(Name = "Minimum Balance")]
        public decimal MinBalance { get; set; }

        public SavingsConfigurationViewModels(SavingsConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                status = config.status;
                CreditInterestRate = config.CreditInterestRate;
                //GlAccount = config.GlAccount;
                MinBalance = config.MinBalance;
                GlAccountId = config.GlAccountId;
                SavingsInterestPayableGlId = config.SavingsInterestPayableGlId;
            }
        }
    }

}

   
