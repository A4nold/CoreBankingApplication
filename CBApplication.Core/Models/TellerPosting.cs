using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public enum PostingType
    {
        Withdrawal = 1,
        Deposit = 2
    }
    public class TellerPosting
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public PostingType Type { get; set; }

        public CustomerAccount Customer { get; set; }

        public int CustomerId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string Narration { get; set; }

        public DateTime DateOfPosting { get; set; }
    }
}
