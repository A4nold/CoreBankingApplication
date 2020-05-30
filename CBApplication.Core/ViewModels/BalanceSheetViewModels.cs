using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class BalanceSheetViewModels
    {
        public IEnumerable<GlAccountManagement> AssetsGL { get; set; }

        public IEnumerable<GlAccountManagement> LiabiliyGl { get; set; }

        public IEnumerable<GlAccountManagement> CapitalGl { get; set; }

        public decimal AssetSum { get; set; }

        public decimal LiabilitySum { get; set; }

        public decimal CapitalSum { get; set; }

        public decimal CLSum { get; set; }
    }
}
