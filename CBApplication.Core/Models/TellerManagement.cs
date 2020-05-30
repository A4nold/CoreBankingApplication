using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBApplication.Core.Models
{
    public class TellerManagement
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public User User { get; set; }

        [Required]
        [Display(Name = "User")]
        public int userId { get; set; }

        [Display(Name = "Account")]
        public GlAccountManagement AccountManagement { get; set; }

        [Required]
        [Display(Name = "Account")]
        public int AccountManagementId { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
