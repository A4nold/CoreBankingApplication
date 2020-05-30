using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class TellerManagementViewModels
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<GlAccountManagement> Category { get; set; }

        public TellerManagement TellerManagements { get; set; }
    }
}
