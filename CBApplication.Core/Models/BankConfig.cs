using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class BankConfig
    {
        public int Id { get; set; }

        [DefaultValue(true)]
        public bool IsOpen { get; set; }

        public DateTime FinancialDate { get; set; }

        public int DayCount { get; set; }

        public int MonthCount { get; set; }

        public int YearCount { get; set; }
    }
}
