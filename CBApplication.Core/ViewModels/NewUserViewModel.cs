using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class NewUserViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public User user { get; set; }
    }
}
