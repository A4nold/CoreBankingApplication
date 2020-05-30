using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;

namespace CBApplication.Core.ViewModels
{
    public class GlAccountManagementViewModels
    {
        [Display(Name = "Category")]
        public IEnumerable<GlCategoryManagement> Categories { get; set; }

        [Display(Name = "Branch")]
        public IEnumerable<Branch> Branches { get; set; }
        
        [Display(Name ="Account Name")]
        public GlAccountManagement AccountManagement { get; set; }
    }
}
