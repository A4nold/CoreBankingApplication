using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class SavingsConfig
    {
        public int Id { get; set; }

        [Display(Name = "Savings Interest GlAccount")]
        public GlAccountManagement GlAccount { get; set; }

        
        [Display(Name = "Savings Gl Account")]
        public int GlAccountId { get; set; }

        
        public int SavingsInterestPayableGlId { get; set; }

        
        [Display(Name = "Credit Interest Rate")]
        public decimal CreditInterestRate { get; set; }

        
        public bool status { get; set; }

        
        [Display(Name = "Minimum Balance")]
        public decimal MinBalance { get; set; }
    }
}
