using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(225)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

       
        public Branch Branch { get; set; }

        [Display(Name = "Branch")]
        public string Branch_Name { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(15 )]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        public bool PassChanged { get; set; }

        [Required]
        public string Role { get; set; }

        public bool UserAssigned { get; set; }
    }
}
