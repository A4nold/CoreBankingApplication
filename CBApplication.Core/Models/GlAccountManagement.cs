using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class GlAccountManagement
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Gl Account Name")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public GlCategoryManagement CategoryManagement { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryManagementId { get; set; }

        [Display(Name = "Branch")]
        public Branch Branch { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public string BranchId { get; set; }

        public MainAccountCategory MainAccountCategory { get; set; }

        public string MainAccountCategoryId { get; set; }
        
        public long AccCode { get; set; }

        public bool AccountAssigned { get; set; }

        [DefaultValue(0)]
        public decimal AccountBalance { get; set; }
    }
}
