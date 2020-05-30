using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Branch Name")]
        public string BranchName{ get; set; }
    }
}
