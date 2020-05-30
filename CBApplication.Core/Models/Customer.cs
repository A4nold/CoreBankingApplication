using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class Customer
    {
        public int id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Customer ID")]
        public string customerId { get; set; }

        //[DefaultValue(false)]
        //public bool PassChanged { get; set; }

        //public string Role { get; set; }
    }
}
