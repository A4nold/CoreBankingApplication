using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public enum RepaymentPlan
    {
        Daily  = 30
    }

    public class Loans
    {
        public int Id { get; set; }

        public CustomerAccount CustomerAccount { get; set; }

        public string Name { get; set; }

        public int CustomerAccountId { get; set; }

        public int DaysCount { get; set; }

        [Display(Name = "Customer Account Number")]
        public long CustomerAccountNumber { get; set; }

        [Display(Name = "Loan Amount")]
        public decimal Amount { get; set; }

        public decimal Interest { get; set; }//Amount * interestrate/100

        public decimal InterestRate { get; set; }//Gotten from loan config

        public decimal DailyLoanDeduction { get; set; }//Amount / duration in days

        public decimal InterestDeduction { get; set; }//Interest / duration in days

        public decimal LoanDialyInterestAccrued { get; set; }

        [Display(Name = "Balance")]
        public decimal LoanBalance { get; set; }

        [Display(Name ="Narration")]
        public string Narration { get; set; }

        [Display(Name = "Loan Account Number")]
        public long AccountNumber { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        [Display(Name = "Duration in Months")]
        public int DurationInMonths { get; set; }

        public RepaymentPlan RepaymentPlan { get; set; }

        public bool Status { get; set; }

        public decimal PrincipalRemaining { get; set; }

        public decimal InterestRemaining { get; set; }
    }
}



