using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class CustomerAccountViewModels
    {
        public CustomerAccount CustomerAccount { get; set; }

        public Customer Customers { get; set; }

        public IEnumerable<Branch> Branches { get; set; }
    }
}
