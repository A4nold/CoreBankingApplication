using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class GlPosting
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }


        [Display(Name = "Account to Debit")]
        public GlAccountManagement DebitAccount { get; set; }

        [Display(Name = "Debit Narration")]
        public string DebitAccountNarration { get; set; }

        [Required]
        [Display(Name = "Account to Debit")]
        public int DebitAccountId { get; set; }


        [Display(Name = "Account to Credit")]
        public GlAccountManagement CreditAccount { get; set; }

        [Display(Name = "Credit Narration")]
        public string CreditAccountNarration { get; set; }

        [Required]
        [Display(Name = "Account to Credit")]
        public int CreditAccountId { get; set; }


        [Display(Name = "User")]
        public User User { get; set; }


        [Display(Name = "User")]
        public int UserId { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }

    }
}
