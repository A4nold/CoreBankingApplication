using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class GlPostingViewModels
    {
        [Display(Name = "Account to Credit")]
        public IEnumerable<GlAccountManagement> CreditAccount { get; set; }

        public int CreditAccountId { get; set; }

        [Display(Name = "Account to Debit")]
        public IEnumerable<GlAccountManagement> DebitAccount { get; set; }

        public int DebitAccountId { get; set; }

        public GlPosting GlPosting { get; set; }
    }
}
