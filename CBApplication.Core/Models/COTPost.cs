using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class COTPost
    {
        public int Id { get; set; }

        public CustomerAccount CustomerAccount { get; set; }

        public int CustomerAccountId { get; set; }

        public GlAccountManagement GlAccount { get; set; }

        public int GlAccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime WhenPosted { get; set; }
    }
}
