using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class GlCategoryManagement
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(40)]
        [Required(ErrorMessage = "Category Name is Required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(225)]
        [Required(ErrorMessage = "Please Write a Description")]
        public string Description { get; set; }

        public MainAccountCategory MainAccountCategory { get; set; }

        [Display(Name = "Category")]
        public string MainAccountCategoryId { get; set; }
    }
}
