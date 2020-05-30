using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public enum AccountType
    {
        Savings = 1,
        Current = 2,
        Loan =3
    }

    public class CustomerAccount
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public Customer Customers { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string customerId { get; set; }

        [Display(Name = "Branch")]
        public Branch Branches { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Display(Name = "Lien")]
        public decimal CurrentLien { get; set; }

        public decimal InterestGot { get; set; }

        [Display(Name = "Branch")]
        public string branchName { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public long AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name="isClosed")]
        public bool isClosed { get; set; }

        [Display(Name="Balance")]
        public decimal Balance { get; set; }

        public decimal CotAccured { get; set; }
    }
}
